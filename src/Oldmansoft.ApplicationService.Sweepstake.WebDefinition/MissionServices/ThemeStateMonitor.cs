using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.WebDefinition.MissionServices
{
    class ThemeStateMonitor : Util.LoopExecutor, IMission
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get
            {
                return "主题状态监控器";
            }
        }

        /// <summary>
        /// 默认值
        /// </summary>
        public Model.MissionDefaultValue Default
        {
            get
            {
                return new Model.MissionDefaultValue(DataDefinition.StartupType.Automatic, 30);
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Execute()
        {
            new Application.Theme().StateMonitor(this);
        }
    }
}
