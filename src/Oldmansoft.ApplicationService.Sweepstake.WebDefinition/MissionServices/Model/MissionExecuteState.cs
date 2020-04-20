using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.WebDefinition.MissionServices.Model
{
    /// <summary>
    /// 任务执行状态
    /// </summary>
    public enum MissionExecuteState
    {
        /// <summary>
        /// 空闲
        /// </summary>
        [Description("空闲")]
        Idle,

        /// <summary>
        /// 忙碌
        /// </summary>
        [Description("忙碌")]
        Busy
    }
}
