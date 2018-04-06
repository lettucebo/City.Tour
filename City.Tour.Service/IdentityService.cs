using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using City.Tour.Library.Models.CityTour;
using City.Tour.Service.Base;
using Microsoft.Owin.Security;

namespace City.Tour.Service
{
    public class IdentityService : BaseService
    {
        private static string IdentityProvider =
            "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider";

        public static void Authentication(IAuthenticationManager authenticationManager, User user)
        {
            var identity = new ClaimsIdentity(
                new[]
                {
                    new Claim(IdentityProvider, user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim("ProfileId", user.ProfileId),
                    new Claim("Picture", user.Picture),
                    new Claim("Source", user.Source),
                    new Claim("TeamId", user.TeamId.ToString()),
                    new Claim("IsAdmin", user.IsAdmin.ToString())
                }, "CITYTOUR");


            authenticationManager.SignIn(
                new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddDays(14) },
                identity);
        }

        /// <summary>
        /// 登出所有相關INXGPS站台
        /// </summary>
        /// <param name="authManager"></param>
        public static void Logoff(IAuthenticationManager authManager)
        {
            authManager.SignOut("CITYTOUR");
            HttpContext.Current.Session.Abandon();
        }
    }
}
