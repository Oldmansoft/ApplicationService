using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.DataDefinition
{
    /// <summary>
    /// 客户端内容
    /// </summary>
    public class ClientContent
    {
        /// <summary>
        /// 客户端序号
        /// </summary>
        public Guid AppId { get; private set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string Order { get; private set; }

        private ClientContent() { }

        public static ClientContent Create(Guid appId, string order)
        {
            if (appId == Guid.Empty) throw new ArgumentNullException("appId");
            if (string.IsNullOrWhiteSpace(order)) throw new ArgumentNullException("order");

            var result = new ClientContent();
            result.AppId = appId;
            result.Order = order.Trim();
            return result;
        }

        public static ClientContent CreateEmpty()
        {
            var result = new ClientContent();
            result.AppId = Guid.Empty;
            result.Order = Guid.NewGuid().ToString("N");
            return result;
        }
    }
}
