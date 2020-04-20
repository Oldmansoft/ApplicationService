using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Oldmansoft.ApplicationService.MoneyBag.WebApp.Controllers
{
    public class WalletExpendController : ApiController
    {
        public DataDefinition.TradeState Put(Guid id, [FromBody]Models.TradeRequestModel model)
        {
            long transactionId;
            return new Application.Money().Expend(id, model.ClientAppId, model.ClientOrder, model.Cent, model.Description, model.Callback, out transactionId);
        }

        public Models.TradeResponseModel Post(Guid id, [FromBody]Models.TradeRequestModel model)
        {
            long transactionId;
            var result = new Models.TradeResponseModel();
            result.State = new Application.Money().Expend(id, model.ClientAppId, model.ClientOrder, model.Cent, model.Description, model.Callback, out transactionId);
            result.TransactionId = transactionId;
            return result;
        }
    }
}
