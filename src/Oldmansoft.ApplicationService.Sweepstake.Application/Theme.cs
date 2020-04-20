using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Application
{
    public class Theme : ApplicationBase
    {
        public void StateMonitor(DataDefinition.IExecutor executor)
        {
            new Services.SweepstakeThemeService(Factory).StateMonitor(executor);
        }

        public DataDefinition.SweepstakeThemeAddMemberResult AddMember(Guid themeId, Guid memberId)
        {
            return new Services.SweepstakeThemeService(Factory).AddMember(themeId, memberId);
        }

        public bool Win(Guid themeId, out Data.ThemeData theme)
        {
            Domain.SweepstakeTheme domain;
            var result = new Services.SweepstakeThemeService(Factory).Win(themeId, out domain);
            theme = domain.MapTo(new Data.ThemeData());
            return result;
        }

        public bool Add(string name, DateTime start, DateTime finish, int floor, int ceiling, List<int> definition)
        {
            var domain = Domain.SweepstakeTheme.Create(name, start, finish, floor, ceiling, definition);
            Factory.CreateSweepstakeThemeRepository().Add(domain);
            Factory.GetUnitOfWork().Commit();
            return true;
        }

        public bool Change(Guid id, string name, DateTime start, DateTime finish, int floor, int ceiling, List<int> definition)
        {
            var repository = Factory.CreateSweepstakeThemeRepository();
            var domain = repository.Get(id);
            if (domain == null) return false;
            domain.Change(name, start, finish, floor, ceiling, definition);
            repository.Replace(domain);
            Factory.GetUnitOfWork().Commit();
            return true;
        }

        public Data.ThemeData Get(Guid id)
        {
            var domain = Factory.CreateSweepstakeThemeRepository().Get(id);
            return domain.MapTo(new Data.ThemeData());
        }

        public int Count()
        {
            return Factory.CreateSweepstakeThemeRepository().Count();
        }

        public IList<Data.ThemeData> Page(int index, int size, out int count)
        {
            var list = Factory.CreateSweepstakeThemeRepository().Page().Size(size).ToList(index, out count);
            return list.MapTo(new List<Data.ThemeData>());
        }
    }
}
