using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.DataDefinition
{
    public class CallbackPostContent
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// 应用号
        /// </summary>
        public Guid AppId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 事务号
        /// </summary>
        public long TransactionId { get; set; }

        /// <summary>
        /// 分
        /// </summary>
        public int Paid { get; set; }

        public CallbackPostContent(Guid accountId, Guid appId, string order, long transactionId, int cent)
        {
            AccountId = accountId;
            AppId = appId;
            Order = order;
            TransactionId = transactionId;
            Paid = cent;
        }
    }
}
