using Oldmansoft.ApplicationService.MoneyBag.Domain;
using Oldmansoft.ApplicationService.MoneyBag.Infrastructure;
using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Repositories
{
    public class AppClientRepository : ClassicDomain.Driver.Mongo.Repository<AppClient, Guid, MongoMapping>, IAppClientRepository
    {
        public AppClientRepository(UnitOfWork uow)
            : base(uow)
        { }

        public IPagingData<AppClient> Paging()
        {
            return Query().Paging().OrderByDescending(o => o.Id);
        }
    }
}
