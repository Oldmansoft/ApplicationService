using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.Data
{
    public class AppClientData
    {
        public Guid Id { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; private set; }
    }
}
