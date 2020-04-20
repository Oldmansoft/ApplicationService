using Oldmansoft.ApplicationService.Sweepstake.Domain;
using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Repositories
{
    class SweepstakeMissionRepository : ClassicDomain.Driver.Mongo.Repository<SweepstakeMission, string, Mapping>, Infrastructure.ISweepstakeMissionRepository
    {
        public SweepstakeMissionRepository(UnitOfWork uow)
            : base(uow)
        { }

        public IList<SweepstakeMission> List()
        {
            return Query().ToList();
        }
    }
}
