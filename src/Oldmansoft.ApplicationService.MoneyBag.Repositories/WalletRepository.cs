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
    class WalletRepository : ClassicDomain.Driver.Mongo.Repository<Wallet, Guid, MongoMapping>, IWalletRepository
    {
        public WalletRepository(UnitOfWork uow)
            : base(uow)
        { }

        public long All()
        {
            return Query().Select(o => o.Cent).ToList().Sum(o => o);
        }
    }
}
