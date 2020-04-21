using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Oldmansoft.ApplicationService.MoneyBag.WebApp.Controllers
{
    public class WalletRechargeController : ApiController
    {
        public int Get(Guid id, int beforeDays)
        {
            return new Application.Money().CountRecharge(id, beforeDays);
        }

        public bool Put(Guid id, [FromBody]Models.TradeRequestModel model)
        {
            long transactionId;
            return new Application.Money().Recharge(id, model.ClientAppId, model.ClientOrder, model.Value, model.Memo, model.Callback, out transactionId);
        }

        public Models.TradeResponseModel Post(Guid id, [FromBody]Models.TradeRequestModel model)
        {
            long transactionId;
            var rechargeResult = new Application.Money().Recharge(id, model.ClientAppId, model.ClientOrder, model.Value, model.Memo, model.Callback, out transactionId);
            var result = new Models.TradeResponseModel();
            result.State = rechargeResult ? DataDefinition.TradeState.Success : DataDefinition.TradeState.ExistClientOrder;
            result.TransactionId = transactionId;
            return result;
        }
    }
}
