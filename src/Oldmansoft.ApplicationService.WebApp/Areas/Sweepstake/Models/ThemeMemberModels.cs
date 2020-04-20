using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Oldmansoft.ApplicationService.WebApp.Areas.Sweepstake.Models
{
    public class ThemeMemberAddModel
    {
        public Guid ThemeId { get; set; }

        public Guid MemberId { get; set; }
    }
}