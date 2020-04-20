using System.Web.Mvc;

namespace Oldmansoft.ApplicationService.WebApp.Areas.Sweepstake
{
    public class SweepstakeAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Sweepstake";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Sweepstake_default",
                "Sweepstake/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}