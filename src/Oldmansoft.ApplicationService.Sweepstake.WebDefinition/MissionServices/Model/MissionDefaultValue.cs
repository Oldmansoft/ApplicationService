using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.WebDefinition.MissionServices.Model
{
    /// <summary>
    /// 任务默认值
    /// </summary>
    public class MissionDefaultValue
    {
        /// <summary>
        /// 类型
        /// </summary>
        public DataDefinition.StartupType Type { get; set; }

        /// <summary>
        /// 休眠
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="type"></param>
        /// <param name="seconds"></param>
        public MissionDefaultValue(DataDefinition.StartupType type, int seconds)
        {
            Type = type;
            Seconds = seconds;
        }
    }
}
