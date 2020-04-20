using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.DataDefinition
{
    /// <summary>
    /// 锁值
    /// </summary>
    public class LockValue
    {
        public Guid AppId { get; private set; }

        public string Order { get; private set; }

        public DateTime Created { get; private set; }

        private LockValue()
        {
            Created = DateTime.UtcNow;
        }

        public static LockValue Create(Guid appId, string order)
        {
            var result = new LockValue();
            result.AppId = appId;
            result.Order = order;
            return result;
        }

        public bool IsMime(Guid appId, string order)
        {
            return AppId == appId && Order == order;
        }
    }
}
