using System.Web.Mvc;

namespace City.Tour.Web.Areas.Admins
{
    public class AdminsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admins";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admins_default",
                "Admins/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "City.Tour.Web.Areas.Admins.Controllers" }
            );
        }
    }
}