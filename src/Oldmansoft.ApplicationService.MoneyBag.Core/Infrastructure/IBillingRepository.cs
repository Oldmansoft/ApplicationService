using Oldmansoft.ApplicationService.MoneyBag.Domain;
using Oldmansoft.ClassicDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Infrastructure
{
    public interface IBillingRepository : IRepository<Billing, long>
    {
        Billing GetLastBefore(Guid accountId, long beforeBillId);

        Billing GetSwitchTarget(long billingId);

        bool HasAppClientId(Guid id);

        long GetLashId();

        IPagingData<Billing> Paging();

        IPagingData<Billing> Paging(Guid accountId);

        IList<Billing> List(Guid accountId, int skip, int count);

        IList<Billing> List(Guid accountId, DateTime start, DateTime finish);

        Billing GetByClient(Guid clientAppId, string clientOrdreId);

        int CountRecharge(Guid accountId, int beforeDays);
    }
}
