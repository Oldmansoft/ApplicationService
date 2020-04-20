using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Application
{
    public class AppClient : ApplicationBase
    {
        public Data.AppClientData Get(Guid id)
        {
            var repository = Factory.CreateAppClientRepository();
            return repository.Get(id).MapTo(new Data.AppClientData());
        }

        public Guid Create(string name, string description)
        {
            var repository = Factory.CreateAppClientRepository();
            var domain = Domain.AppClient.Create(name, description);
            repository.Add(domain);
            Factory.GetUnitOfWork().Commit();
            return domain.Id;
        }

        public Guid Create(Guid id, string name, string description)
        {
            var repository = Factory.CreateAppClientRepository();
            var domain = Domain.AppClient.Create(id, name, description);
            repository.Add(domain);
            Factory.GetUnitOfWork().Commit();
            return domain.Id;
        }

        public void Change(Guid id, string name, string description)
        {
            var repository = Factory.CreateAppClientRepository();
            var domain = repository.Get(id);
            if (domain == null) return;
            domain.Change(name, description);
            repository.Replace(domain);
            Factory.GetUnitOfWork().Commit();
        }

        public bool Remove(Guid id)
        {
            if (Factory.CreateBillingRepository().HasAppClientId(id)) return false;

            var repository = Factory.CreateAppClientRepository();
            var domain = repository.Get(id);
            if (domain == null) return true;
            repository.Remove(domain);
            Factory.GetUnitOfWork().Commit();
            return true;
        }

        public IList<Data.AppClientData> Paging(int index, int size, out int totalCount)
        {
            var repository = Factory.CreateAppClientRepository();
            var list = repository.Paging().Size(size).ToList(index, out totalCount);
            return list.MapTo(new List<Data.AppClientData>());
        }
    }
}
