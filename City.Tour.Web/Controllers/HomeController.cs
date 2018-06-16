using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ci.Mvc.Sort.Models;
using City.Tour.Service;

namespace City.Tour.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Login", "Account");
        }
    }
}