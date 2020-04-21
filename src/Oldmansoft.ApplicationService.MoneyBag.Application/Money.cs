using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Application
{
    public class Money : ApplicationBase
    {
        public bool Recharge(Guid accountId, Guid clientAppId, string clientOrder, int value, string memo, string callback, out long transactionId)
        {
            if (Factory.CreateAppClientRepository().Get(clientAppId) == null)
            {
                throw new ArgumentException(string.Format("clientAppId 不存在 {0}", clientAppId));
            }
            var trade = new Services.TradeService(Factory);
            return trade.Recharge(accountId, clientAppId, clientOrder, value, memo, callback, out transactionId);
        }

        public DataDefinition.TradeState Expend(Guid accountId, Guid clientAppId, string clientOrder, int value, string memo, string callback, out long transactionId)
        {
            if (Factory.CreateAppClientRepository().Get(clientAppId) == null)
            {
                throw new ArgumentException(string.Format("clientAppId 不存在 {0}", clientAppId));
            }
            var trade = new Services.TradeService(Factory);
            return trade.Expend(accountId, clientAppId, clientOrder, value, memo, callback, out transactionId);
        }

        public DataDefinition.TradeState Chargeback(Guid accountId, Guid clientAppId, string clientOrder, int value, string memo, string callback, out long transactionId)
        {
            if (Factory.CreateAppClientRepository().Get(clientAppId) == null)
            {
                throw new ArgumentException(string.Format("clientAppId 不存在 {0}", clientAppId));
            }
            var trade = new Services.TradeService(Factory);
            return trade.Chargeback(accountId, clientAppId, clientOrder, value, memo, callback, out transactionId);
        }

        public DataDefinition.TradeState Transfer(Guid sourceAccountId, Guid targetAccountId, int value, string memo)
        {
            var trade = new Services.TradeService(Factory);
            return trade.Transfer(sourceAccountId, targetAccountId, value, memo);
        }

        public int CountRecharge(Guid accountId, int beforeDays)
        {
            return Factory.CreateBillingRepository().CountRecharge(accountId, beforeDays);
        }
    }
}
