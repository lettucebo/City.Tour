using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ci.Extension;
using Ci.Mvc.Alert;
using Ci.Mvc.Sort.Models;
using Ci.Upload.Extensions;
using City.Tour.Library.Models.CityTour;
using City.Tour.Service;
using City.Tour.Web.Base;
using DateTime = System.DateTime;

namespace City.Tour.Web.Areas.Admins.Controllers
{
    public class PuzzleController : BaseAdminController
    {
        private TourService tourService = new TourService();
        private PuzzleService puzzleService = new PuzzleService();

        public ActionResult Index(Guid tourId, string keyword, SortOrder sort = default(SortOrder), int page = 1)
        {
            var tour = tourService.GetById(tourId);
            if (tour == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var datas = puzzleService.ListAllToPagedAndFilterSort(tourId, keyword, sort, page);

            ViewBag.sort = sort;
            ViewBag.tour = tour;

            return View(datas);
        }

        public ActionResult Create(Guid tourId)
        {
            var tour = tourService.GetById(tourId);
            if (tour == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.tour = tour;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Guid tourId, Puzzle model, HttpPostedFileBase image)
        {
            var tour = tourService.GetById(tourId);
            if (tour == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.tour = tour;

            if (model.Name.IsNullOrWhiteSpace())
            {
                this.SetAlert("問題不可為空！");
                return View(model);
            }

            if (!image.IsNullOrEmpty())
            {
                var file = image.SaveAsLocal("Images/Puzzle");
                model.Picture = file.VirtualPath;
            }

            model.Id = Ci.Sequential.Guid.Create();
            model.CreateTime = DateTime.Now;
            model.ModifyTime = DateTime.Now;
            model.TourId = tourId;

            puzzleService.Create(model);
            this.SetAlert($"謎題：{model.Name}，新增成功！");
            return RedirectToAction("Index", "Puzzle", new { tourId });
        }

        public ActionResult Edit(Guid puzzleId)
        {
            var data = puzzleService.GetById(puzzleId);
            if (data == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var tour = tourService.GetById(data.TourId);
            if (tour == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.tour = tour;

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Guid puzzleId, Puzzle model, HttpPostedFileBase image)
        {
            var tour = tourService.GetById(model.TourId);
            if (tour == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.tour = tour;

            if (model.Name.IsNullOrWhiteSpace())
            {
                this.SetAlert("問題不可為空！");
                return View(model);
            }

            if (!image.IsNullOrEmpty())
            {
                var file = image.SaveAsLocal("Images/Puzzle");
                model.Picture = file.VirtualPath;
            }

            puzzleService.Update(model);
            this.SetAlert($"謎題：{model.Name}，更新成功！");
            return RedirectToAction("Index", "Puzzle", new { tourId = model.TourId });
        }
    }
}