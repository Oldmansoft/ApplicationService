using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.DataDefinition
{
    /// <summary>
    /// 启动类型
    /// </summary>
    public enum StartupType
    {
        [Description("自动")]
        Automatic,

        [Description("手动")]
        Manual,

        [Description("禁用")]
        Disabled
    }
}
