using Oldmansoft.ApplicationService.Sweepstake.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Infrastructure
{
    public interface ISweepstakeMissionRepository : ClassicDomain.IRepository<SweepstakeMission, string>
    {
        IList<SweepstakeMission> List();
    }
}
