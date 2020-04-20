using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Oldmansoft.ApplicationService.WebApp.Areas.Sweepstake.Controllers
{
    public class ThemeMemberController : ApiController
    {
        public int Get(Guid id)
        {
            return new ApplicationService.Sweepstake.Application.ThemeMember().Count(id);
        }

        public IList<ApplicationService.Sweepstake.Data.ThemeMemberData> Get(Guid id, int skip, int count)
        {
            return new ApplicationService.Sweepstake.Application.ThemeMember().List(id, skip, count);
        }

        public void Post(Models.ThemeMemberAddModel model)
        {
            new ApplicationService.Sweepstake.Application.Theme().AddMember(model.ThemeId, model.MemberId);
        }
    }
}
