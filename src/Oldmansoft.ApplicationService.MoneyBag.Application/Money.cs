using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Application
{
    public class Money : ApplicationBase
    {
        public bool Recharge(Guid accountId, Guid clientAppId, string clientOrderId, int cent, string description, string callback, out long transactionId)
        {
            if (Factory.CreateAppClientRepository().Get(clientAppId) == null)
            {
                throw new ArgumentException(string.Format("clientAppId 不存在 {0}", clientAppId));
            }
            var trade = new Services.TradeService(Factory);
            return trade.Recharge(accountId, clientAppId, clientOrderId, cent, description, callback, out transactionId);
        }

        public DataDefinition.TradeState Expend(Guid accountId, Guid clientAppId, string clientOrderId, int cent, string description, string callback, out long transactionId)
        {
            if (Factory.CreateAppClientRepository().Get(clientAppId) == null)
            {
                throw new ArgumentException(string.Format("clientAppId 不存在 {0}", clientAppId));
            }
            var trade = new Services.TradeService(Factory);
            return trade.Expend(accountId, clientAppId, clientOrderId, cent, description, callback, out transactionId);
        }

        public DataDefinition.TradeState Chargeback(Guid accountId, Guid clientAppId, string clientOrderId, int cent, string description, string callback, out long transactionId)
        {
            if (Factory.CreateAppClientRepository().Get(clientAppId) == null)
            {
                throw new ArgumentException(string.Format("clientAppId 不存在 {0}", clientAppId));
            }
            var trade = new Services.TradeService(Factory);
            return trade.Chargeback(accountId, clientAppId, clientOrderId, cent, description, callback, out transactionId);
        }

        public DataDefinition.TradeState Transfer(Guid sourceAccountId, Guid targetAccountId, int cent, string description)
        {
            var trade = new Services.TradeService(Factory);
            return trade.Transfer(sourceAccountId, targetAccountId, cent, description);
        }

        public int CountRecharge(Guid accountId, int beforeDays)
        {
            return Factory.CreateBillingRepository().CountRecharge(accountId, beforeDays);
        }
    }
}
