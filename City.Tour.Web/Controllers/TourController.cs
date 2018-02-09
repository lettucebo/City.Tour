using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace City.Tour.Web.Controllers
{
    public class TourController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}