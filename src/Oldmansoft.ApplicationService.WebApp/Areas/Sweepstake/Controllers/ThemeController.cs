using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Oldmansoft.ApplicationService.WebApp.Areas.Sweepstake.Controllers
{
    public class ThemeController : ApiController
    {
        public ApplicationService.Sweepstake.Data.ThemeData Get(Guid id)
        {
            return new ApplicationService.Sweepstake.Application.Theme().Get(id);
        }

        public int Get()
        {
            return new ApplicationService.Sweepstake.Application.Theme().Count();
        }

        public IList<ApplicationService.Sweepstake.Data.ThemeData> Get(int index, int size)
        {
            int count;
            return new ApplicationService.Sweepstake.Application.Theme().Page(index, size, out count);
        }

        public void Post(Models.ThemeAddModel model)
        {
            new ApplicationService.Sweepstake.Application.Theme().Add(model.Name, model.Start, model.Finish, model.Floor, model.Ceiling, model.Definition);
        }

        public void Put(Guid id, Models.ThemePutModel model)
        {
            new ApplicationService.Sweepstake.Application.Theme().Change(id, model.Name, model.Start, model.Finish, model.Floor, model.Ceiling, model.Definition);
        }
    }
}
