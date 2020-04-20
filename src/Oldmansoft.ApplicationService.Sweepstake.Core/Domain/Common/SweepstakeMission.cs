using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Domain
{
    /// <summary>
    /// 任务
    /// </summary>
    public class SweepstakeMission
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// 启动类型
        /// </summary>
        public DataDefinition.StartupType Type { get; private set; }

        /// <summary>
        /// 休眠秒数
        /// </summary>
        public int Seconds { get; private set; }

        private SweepstakeMission() { }

        public static SweepstakeMission Create(string id, DataDefinition.StartupType Type, int seconds)
        {
            var result = new SweepstakeMission();
            result.Id = id;
            result.Type = Type;
            result.Seconds = seconds;
            return result;
        }

        public void Change(DataDefinition.StartupType type, int seconds)
        {
            Type = type;
            Seconds = seconds;
        }
    }
}
