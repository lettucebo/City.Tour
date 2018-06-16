using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Ci.Extension;
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
        private TeamService teamService = new TeamService();

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                var teamId = new Guid(((ClaimsPrincipal)User).FindFirst("TeamId").Value ?? Guid.Empty.ToString());
                return RedirectToAction("Index", "Tour", new { teamId = teamId });
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult FbLogin(User model, string teamCode)
        {
            if (teamCode.IsNullOrWhiteSpace())
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "找不到團隊資料");

            var team = teamService.GetByInviteCode(teamCode);
            if (team == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "找不到團隊資料");

            model.Source = "Facebook";
            model.TeamId = team.Id;
            var user = userService.CheckOrCreate(model);

            if (user.TeamId != team.Id)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, $"此帳號已綁定團隊: {team.Name}");

            IdentityService.Authentication(AuthenticationManager, user);

            var retrnUrl = Url.Action("Index", "Tour", new { teamId = team.Id }, Request.Url.Scheme);
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