using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Data
{
    /// <summary>
    /// 活动成员
    /// </summary>
    public class ThemeMemberData
    {
        public Guid Id { get; private set; }

        /// <summary>
        /// 主题号
        /// </summary>
        public Guid ThemeId { get; private set; }

        /// <summary>
        /// 成员号
        /// </summary>
        public Guid MemberId { get; private set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// 创建
        /// </summary>
        public DateTime Created { get; private set; }
    }
}
