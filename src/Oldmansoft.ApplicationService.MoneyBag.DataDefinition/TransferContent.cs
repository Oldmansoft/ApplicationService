using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.DataDefinition
{
    /// <summary>
    /// 转帐内容
    /// </summary>
    public class TransferContent
    {
        /// <summary>
        /// 帐号
        /// </summary>
        public Guid AccountId { get; private set; }

        /// <summary>
        /// 单号
        /// </summary>
        public long BillingId { get; private set; }

        private TransferContent() { }

        public static TransferContent Create(Guid accountId, long billingId)
        {
            var result = new TransferContent();
            result.AccountId = accountId;
            result.BillingId = billingId;
            return result;
        }
    }
}
