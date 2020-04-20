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
        public string OrderId { get; private set; }

        private ClientContent() { }

        public static ClientContent Create(Guid appId, string orderId)
        {
            if (appId == Guid.Empty) throw new ArgumentNullException("appId");
            if (string.IsNullOrWhiteSpace(orderId)) throw new ArgumentNullException("orderId");

            var result = new ClientContent();
            result.AppId = appId;
            result.OrderId = orderId.Trim();
            return result;
        }

        public static ClientContent CreateEmpty()
        {
            var result = new ClientContent();
            result.AppId = Guid.Empty;
            result.OrderId = Guid.NewGuid().ToString("N");
            return result;
        }
    }
}
