using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oldmansoft.ApplicationService.Sweepstake.Domain
{
    /// <summary>
    /// 活动成员
    /// </summary>
    public class SweepstakeThemeMember
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

        private SweepstakeThemeMember()
        {
            Created = DateTime.UtcNow;
        }

        public static SweepstakeThemeMember Create(SweepstakeTheme theme, Guid memberId, int number)
        {
            if (theme == null) throw new ArgumentNullException("theme");
            var result = new SweepstakeThemeMember();
            result.ThemeId = theme.Id;
            result.MemberId = memberId;
            result.Number = number;
            return result;
        }
    }
}
