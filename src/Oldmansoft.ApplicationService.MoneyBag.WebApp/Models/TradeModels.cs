using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oldmansoft.ApplicationService.MoneyBag.WebApp.Models
{
    public class TradeRequestModel
    {
        public Guid ClientAppId { get; set; }

        public string ClientOrder { get; set; }

        public int Value { get; set; }

        public string Memo { get; set; }

        public string Callback { get; set; }
    }

    public class TradeResponseModel
    {
        public DataDefinition.TradeState State { get; set; }

        public long TransactionId { get; set; }
    }

    public class TransferRequestModel
    {
        public Guid TargetId { get; set; }

        public int Value { get; set; }

        public string Memo { get; set; }
    }
}