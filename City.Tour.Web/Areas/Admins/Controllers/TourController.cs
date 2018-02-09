using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ci.Extension;
using Ci.Mvc.Alert;
using Ci.Mvc.Sort.Models;
using City.Tour.Service;
using City.Tour.Web.Base;

namespace City.Tour.Web.Areas.Admins.Controllers
{
    public class TourController : BaseAdminController
    {
        private TourService tourService = new TourService();

        public ActionResult Index(string keyword = "", SortOrder sort = null, int page = 1)
        {
            var datas = tourService.ListAllToPagedAndFilterSort(keyword, sort, page);

            ViewBag.sort = sort;

            return View(datas);
        }

        public ActionResult Edit(Guid id)
        {
            var data = tourService.GetById(id);
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, Library.Models.CityTour.Tour model, HttpPostedFileBase image)
        {
            var data = tourService.GetById(id);

            if (model.Name.IsNullOrWhiteSpace())
            {
                this.SetAlert("名稱為必填！");
            }

            //if(image.)

            return View(data);
        }
    }
}