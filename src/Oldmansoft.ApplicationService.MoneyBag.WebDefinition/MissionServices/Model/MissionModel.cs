using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.MoneyBag.WebDefinition.MissionServices.Model
{
    /// <summary>
    /// 任务
    /// </summary>
    public class MissionModel
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public MissionRunState State { get; set; }

        /// <summary>
        /// 内部
        /// </summary>
        public MissionExecuteState Inner { get; set; }

        /// <summary>
        /// 休眠
        /// </summary>
        public int Seconds { get; set; }
    }
}
