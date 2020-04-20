using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oldmansoft.ApplicationService.Sweepstake.Infrastructure;

namespace Oldmansoft.ApplicationService.Sweepstake.Repositories
{
    public class Factory : IRepositoryFactory
    {
        private ClassicDomain.UnitOfWork Uow { get; set; }

        public Factory()
        {
            Uow = new ClassicDomain.UnitOfWork();
        }

        public ClassicDomain.IUnitOfWork GetUnitOfWork()
        {
            return Uow;
        }

        public ISweepstakeMissionRepository CreateSweepstakeMissionRepository()
        {
            return new SweepstakeMissionRepository(Uow);
        }

        public ISweepstakeThemeRepository CreateSweepstakeThemeRepository()
        {
            return new SweepstakeThemeRepository(Uow);
        }

        public ISweepstakeThemeMemberRepository CreateSweepstakeThemeMemberRepository()
        {
            return new SweepstakeThemeMemberRepository(Uow);
        }
    }
}
