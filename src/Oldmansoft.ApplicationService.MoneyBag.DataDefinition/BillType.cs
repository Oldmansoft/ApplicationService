using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.DataDefinition
{
    public enum BillType
    {
        /// <summary>
        /// 进
        /// </summary>
        [Description("转入")]
        In = 0,

        /// <summary>
        /// 出
        /// </summary>
        [Description("转出")]
        Out = 1,

        /// <summary>
        /// 花费
        /// </summary>
        [Description("支出")]
        Expend = 2,

        /// <summary>
        /// 充值
        /// </summary>
        [Description("充入")]
        Recharge = 3
    }
}
