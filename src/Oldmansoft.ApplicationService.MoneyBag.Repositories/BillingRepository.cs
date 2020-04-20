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
    class BillingRepository : ClassicDomain.Driver.Mongo.Repository<Billing, long, MongoMapping>, IBillingRepository
    {
        public BillingRepository(UnitOfWork uow)
            : base(uow)
        { }

        public Billing GetLastBefore(Guid accountId, long beforeBillId)
        {
            if (beforeBillId == 0)
            {
                return Query().Where(o => o.AccountId == accountId).OrderBy(o => o.Id).LastOrDefault();
            }
            else
            {
                return Query().Where(o => o.AccountId == accountId && o.Id < beforeBillId).OrderBy(o => o.Id).LastOrDefault();
            }
        }

        public Billing GetSwitchTarget(long billingId)
        {
            return Query().Where(o => o.TransferTarget.BillingId == billingId).FirstOrDefault();
        }

        public bool HasAppClientId(Guid id)
        {
            return Query().FirstOrDefault(o => o.Client.AppId == id) != null;
        }

        public long GetLashId()
        {
            var domain = Query().LastOrDefault();
            if (domain == null) return 0;
            else return domain.Id;
        }

        public IPagingData<Billing> Paging()
        {
            return Query().Paging().Where(o => !o.Broken).OrderByDescending(o => o.Id);
        }

        public IPagingData<Billing> Paging(Guid accountId)
        {
            return Query().Paging().Where(o => o.AccountId == accountId && !o.Broken).OrderByDescending(o => o.Id);
        }

        public IList<Billing> List(Guid accountId, int skip, int count)
        {
            var query = Query().Where(o => o.AccountId == accountId).OrderByDescending(o => o.Id);
            if (skip > 0) return query.Skip(skip).Take(count).ToList();
            return query.Take(count).ToList();
        }

        public IList<Billing> List(Guid accountId, DateTime start, DateTime finish)
        {
            return Query().Where(o => o.AccountId == accountId && o.Created > start && o.Created < finish).OrderBy(o => o.Created).ToList();
        }

        public Billing GetByClient(Guid clientAppId, string clientOrder)
        {
            var domain = Query().Where(o => o.Client.AppId == clientAppId && o.Client.Order == clientOrder && !o.Broken).FirstOrDefault();
            return domain;
        }

        public int CountRecharge(Guid accountId, int beforeDays)
        {
            var result = Query().Where(o => o.AccountId == accountId && o.Type == DataDefinition.BillType.Recharge && o.Created > DateTime.UtcNow.AddDays(-beforeDays))
                .ToList()
                .Sum(o => o.Trade);
            return result;
        }
    }
}
