using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ci.Mvc.Alert;

namespace City.Tour.Web.Base
{
    public class BaseAdminController : BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var identity = (ClaimsIdentity)User.Identity;
            IEnumerable<Claim> claims = identity.Claims;

            bool.TryParse(claims.FirstOrDefault(x => x.Type == "IsAdmin")?.Value, out bool isAdmin);
            if (!isAdmin)
            {
                this.SetAlert("無後台權限！");

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                    new
                    {
                        controller = "Home",
                        action = "Index",
                        area = ""
                    }));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}