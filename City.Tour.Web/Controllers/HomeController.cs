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
        private TourService tourService = new TourService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Story()
        {
            return View();
        }

        public ActionResult Map()
        {
            return View();
        }

        public ActionResult Tour(SortOrder sort = null, int page = 1)
        {
            var datas = tourService.ListAllToPagedAndFilterSort("", sort, page);
            return View(datas);
        }
    }
}