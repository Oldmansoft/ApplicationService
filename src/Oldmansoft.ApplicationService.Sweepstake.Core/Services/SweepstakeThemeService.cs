using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Services
{
    public class SweepstakeThemeService
    {
        private IRepositoryFactory Factory { get; set; }

        public SweepstakeThemeService(IRepositoryFactory factory)
        {
            Factory = factory;
        }

        public DataDefinition.SweepstakeThemeAddMemberResult AddMember(Guid themeId, Guid memberId)
        {
            var themeRepository = Factory.CreateSweepstakeThemeRepository();
            var themeMemberRepository = Factory.CreateSweepstakeThemeMemberRepository();
            using (Util.Locker.Lock(themeId))
            {
                var theme = themeRepository.Get(themeId);
                if (theme == null) throw new ArgumentNullException("themeId");
                if (theme.State != DataDefinition.SweepstakeThemeState.Started) return DataDefinition.SweepstakeThemeAddMemberResult.StopBooked;
                if (themeMemberRepository.Has(theme.Id, memberId)) return DataDefinition.SweepstakeThemeAddMemberResult.Existed;
                if (theme.Book.IsFull())
                {
                    theme.Finish();
                    themeRepository.Replace(theme);
                    Factory.GetUnitOfWork().Commit();
                    return DataDefinition.SweepstakeThemeAddMemberResult.StopBooked;
                }

                int count = themeMemberRepository.Count(theme.Id);
                var themeMember = Domain.SweepstakeThemeMember.Create(theme, memberId, count + 1);
                themeMemberRepository.Add(themeMember);
                theme.Book.SetCurrent(count + 1);
                if (theme.Book.IsFull())
                {
                    theme.Finish();
                }
                themeRepository.Replace(theme);
                try
                {
                    Factory.GetUnitOfWork().Commit();
                }
                catch (ClassicDomain.UniqueException)
                {
                    return DataDefinition.SweepstakeThemeAddMemberResult.Existed;
                }
            }

            return DataDefinition.SweepstakeThemeAddMemberResult.Success;
        }

        public bool Win(Guid themeId, out Domain.SweepstakeTheme theme)
        {
            var themeRepository = Factory.CreateSweepstakeThemeRepository();
            var themeMemberRepository = Factory.CreateSweepstakeThemeMemberRepository();
            using (Util.Locker.Lock(themeId))
            {
                theme = themeRepository.Get(themeId);
                if (theme == null) throw new ArgumentNullException("themeId");
                if (theme.State != DataDefinition.SweepstakeThemeState.Finished)
                {
                    return false;
                }
                theme.Win(themeMemberRepository.ListMemberId(theme.Id));
                themeRepository.Replace(theme);
                Factory.GetUnitOfWork().Commit();
            }
            return true;
        }

        public void StateMonitor(DataDefinition.IExecutor executor)
        {
            var themeRepository = Factory.CreateSweepstakeThemeRepository();
            foreach (var id in themeRepository.ListWaitToStartIds())
            {
                if (executor.RequestStop) continue;
                using (Util.Locker.Lock(id))
                {
                    var theme = themeRepository.Get(id);
                    theme.Start();
                    themeRepository.Replace(theme);
                    Factory.GetUnitOfWork().Commit();
                }
            }

            foreach (var id in themeRepository.ListWaitToFinishIds())
            {
                if (executor.RequestStop) continue;
                using (Util.Locker.Lock(id))
                {
                    var theme = themeRepository.Get(id);
                    theme.Finish();
                    themeRepository.Replace(theme);
                    Factory.GetUnitOfWork().Commit();
                }
            }
        }
    }
}
