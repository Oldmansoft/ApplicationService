using Oldmansoft.ApplicationService.Sweepstake.Infrastructure;
using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake
{
    public interface IRepositoryFactory
    {
        IUnitOfWork GetUnitOfWork();

        ISweepstakeMissionRepository CreateSweepstakeMissionRepository();

        ISweepstakeThemeRepository CreateSweepstakeThemeRepository();

        ISweepstakeThemeMemberRepository CreateSweepstakeThemeMemberRepository();
    }
}
