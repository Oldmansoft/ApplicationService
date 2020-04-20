using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Data
{
    /// <summary>
    /// 任务
    /// </summary>
    public class MissionData
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 启动类型
        /// </summary>
        public DataDefinition.StartupType Type { get; set; }

        /// <summary>
        /// 休眠秒数
        /// </summary>
        public int Seconds { get; set; }
    }
}
