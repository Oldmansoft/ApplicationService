using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.DataDefinition
{
    /// <summary>
    /// 执行器
    /// </summary>
    public interface IExecutor
    {
        /// <summary>
        /// 请求停止
        /// </summary>
        bool RequestStop { get; }
    }
}
