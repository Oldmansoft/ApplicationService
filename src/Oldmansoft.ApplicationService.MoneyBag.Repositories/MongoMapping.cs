using Oldmansoft.ApplicationService.MoneyBag.Domain;
using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Repositories
{
    public class MongoMapping : ClassicDomain.Driver.Mongo.Context
    {
        protected override void OnModelCreating()
        {
            Add<Wallet, Guid>(o => o.Id);

            Add<Billing, long>(o => o.Id)
                .SetUnique(g => g.CreateGroup(o => o.AccountId).Add(o => o.Client.AppId).Add(o => o.Client.Order))
                .SetIndex(o => o.Transfer.BillingId)
                .SetIndex(g => g.CreateGroup(o => o.AccountId).Add(o => o.Type).Add(o => o.Created))
                .SetIndex(g => g.CreateGroup(o => o.AccountId).Add(o => o.Created))
                .SetIndex(g => g.CreateGroup(o => o.AccountId).Add(o => o.Broken));

            Add<AppClient, Guid>(o => o.Id);
        }
    }
}
