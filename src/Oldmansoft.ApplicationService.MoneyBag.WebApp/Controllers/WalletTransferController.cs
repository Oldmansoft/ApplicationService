using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Oldmansoft.ApplicationService.MoneyBag.WebApp.Controllers
{
    public class WalletTransferController : ApiController
    {
        public DataDefinition.TradeState Post(Guid id, [FromBody]Models.TransferRequestModel model)
        {
            return new Application.Money().Transfer(id, model.TargetId, model.Cent, model.Description);
        }
    }
}
