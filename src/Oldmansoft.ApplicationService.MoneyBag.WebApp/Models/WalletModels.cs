using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oldmansoft.ApplicationService.MoneyBag.WebApp.Models
{
    public class WalletLockModel
    {
        public Guid AppId { get; set; }

        public string Order { get; set; }

        public int Value { get; set; }
    }

    public class WalletUnlockModel
    {
        public Guid AppId { get; set; }

        public string Order { get; set; }
    }
}