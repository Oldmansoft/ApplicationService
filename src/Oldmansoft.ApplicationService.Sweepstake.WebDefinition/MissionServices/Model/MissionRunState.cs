using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.WebDefinition.MissionServices.Model
{
    /// <summary>
    /// 任务运行状态
    /// </summary>
    public enum MissionRunState
    {
        /// <summary>
        /// 正在停止
        /// </summary>
        [Description("正在停止")]
        Stopping,

        /// <summary>
        /// 已经停止
        /// </summary>
        [Description("已经停止")]
        Stoped,

        /// <summary>
        /// 正在运行
        /// </summary>
        [Description("运行")]
        Running,
    }
}
