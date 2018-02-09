using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using City.Tour.Library.Models.CityTour;
using City.Tour.Service;
using City.Tour.Web.Base;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using City.Tour.Web.Models;

namespace City.Tour.Web.Controllers
{
    public class AccountController : BaseController
    {
        protected IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        private UserService userService = new UserService();

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult FbLogin(User model)
        {
            model.Source = "Facebook";
            var user = userService.CheckOrCreate(model);
            IdentityService.Authentication(AuthenticationManager, user);

            var retrnUrl = Url.Action("Map", "Home", null, Request.Url.Scheme);
            return Json(retrnUrl);
        }

        public ActionResult Logoff()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            IdentityService.Logoff(authManager);

            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "" });
        }
    }
}