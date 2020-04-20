using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Application
{
    public class Mission : ApplicationBase
    {
        public IList<Data.MissionData> List()
        {
            var list = Factory.CreateSweepstakeMissionRepository().List();
            return list.MapTo(new List<Data.MissionData>());
        }

        public Data.MissionData Get(string id)
        {
            var domain = Factory.CreateSweepstakeMissionRepository().Get(id);
            return domain.MapTo(new Data.MissionData());
        }

        public void Set(string id, DataDefinition.StartupType type, int seconds)
        {
            var repository = Factory.CreateSweepstakeMissionRepository();
            var domain = repository.Get(id);
            if (domain == null)
            {
                domain = Domain.SweepstakeMission.Create(id, type, seconds);
                repository.Add(domain);
            }
            else
            {
                domain.Change(type, seconds);
                repository.Replace(domain);
            }

            try
            {
                Factory.GetUnitOfWork().Commit();
            }
            catch (UniqueException)
            { }
        }
    }
}
