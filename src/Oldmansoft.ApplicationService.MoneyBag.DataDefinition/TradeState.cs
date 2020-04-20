using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.DataDefinition
{
    /// <summary>
    /// 支付状态
    /// </summary>
    public enum TradeState
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success,

        /// <summary>
        /// 余额不足
        /// </summary>
        InsufficientBalance,

        /// <summary>
        /// 客户端订单已经存在
        /// </summary>
        ExistClientOrder,

        /// <summary>
        /// 被锁定
        /// </summary>
        Locked
    }
}
