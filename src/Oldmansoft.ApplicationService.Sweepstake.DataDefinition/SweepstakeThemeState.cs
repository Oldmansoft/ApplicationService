using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.DataDefinition
{
    public enum SweepstakeThemeState
    {
        [Description("取消")]
        Cancelled = 0,

        [Description("等待报名")]
        Created = 1,

        [Description("开始报名")]
        Started = 5,
        
        [Description("结束报名")]
        Finished = 10,

        [Description("完成")]
        Completed = 20
    }
}
