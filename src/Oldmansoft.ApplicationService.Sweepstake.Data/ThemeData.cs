using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Data
{
    public class ThemeData
    {
        public Guid Id { get; private set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 中奖者
        /// </summary>
        public List<List<Guid>> Winner { get; private set; }

        /// <summary>
        /// 预订设定
        /// </summary>
        public DataDefinition.SweepstakeThemeBook Book { get; private set; }

        /// <summary>
        /// 状态
        /// </summary>
        public DataDefinition.SweepstakeThemeState State { get; private set; }

        /// <summary>
        /// 创建
        /// </summary>
        public DateTime Created { get; private set; }
    }
}
