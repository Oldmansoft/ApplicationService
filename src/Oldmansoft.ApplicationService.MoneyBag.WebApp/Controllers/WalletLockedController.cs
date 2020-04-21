using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Oldmansoft.ApplicationService.MoneyBag.WebApp.Controllers
{
    public class WalletLockedController : ApiController
    {
        public DataDefinition.LockValue Get(Guid id)
        {
            return new Application.Account().GetLocked(id);
        }

        public DataDefinition.TradeState Post(Guid id, [FromBody]Models.WalletLockModel model)
        {
            return new Application.Account().LockWallet(id, model.AppId, model.Order, model.Value);
        }

        public bool Put(Guid id, [FromBody]Models.WalletUnlockModel model)
        {
            return new Application.Account().UnlockWallet(id, model.AppId, model.Order);
        }
    }
}
