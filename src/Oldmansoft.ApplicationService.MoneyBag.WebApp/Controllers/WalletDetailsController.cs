using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Oldmansoft.ApplicationService.MoneyBag.WebApp.Controllers
{
    public class WalletDetailsController : ApiController
    {
        public IEnumerable<Data.BillingData> Get(Guid id, int skip, int count)
        {
            return new Application.Account().ListBilling(id, skip, count);
        }

        public IEnumerable<Data.BillingData> Get(Guid id, DateTime start, DateTime finish)
        {
            if (start.Kind == DateTimeKind.Unspecified)
            {
                start = start.ToUniversalTime().AddHours(8);
            }
            if (finish.Kind == DateTimeKind.Unspecified)
            {
                finish = finish.ToUniversalTime().AddHours(8);
            }
            return new Application.Account().ListBilling(id, start, finish);
        }

        public Data.BillingData Get(Guid appId, string order)
        {
            return new Application.Account().GetByClient(appId, order);
        }

        public Data.BillingData Get(long id)
        {
            return new Application.Account().GetByTransaction(id);
        }
    }
}
