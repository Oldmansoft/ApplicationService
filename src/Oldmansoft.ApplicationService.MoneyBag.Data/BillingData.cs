using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Data
{
    public class BillingData
    {
        /// <summary>
        /// 序号
        /// </summary>
        public long Id { get; private set; }

        /// <summary>
        /// 类型
        /// </summary>
        public DataDefinition.BillType Type { get; private set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public Guid AccountId { get; private set; }

        /// <summary>
        /// 客户端序号
        /// </summary>
        public DataDefinition.ClientContent Client { get; private set; }

        /// <summary>
        /// 转帐目标
        /// </summary>
        public DataDefinition.TransferContent TransferTarget { get; private set; }

        /// <summary>
        /// 交易值
        /// </summary>
        public int TradeCent { get; private set; }

        /// <summary>
        /// 交易后钱包的值
        /// </summary>
        public int AfterCent { get; private set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 破碎的
        /// </summary>
        public bool Broken { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; private set; }
    }
}
