using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ci.Extension;
using Ci.Mvc.Alert;
using Ci.Mvc.Sort.Models;
using City.Tour.Library.Models.CityTour;
using City.Tour.Service;
using City.Tour.Web.Base;
using DateTime = System.DateTime;

namespace City.Tour.Web.Areas.Admins.Controllers
{
    public class AnswerController : BaseAdminController
    {
        private AnswerService answerService = new AnswerService();
        private PuzzleService puzzleService = new PuzzleService();

        public ActionResult Index(Guid puzzleId, string keyword = "", SortOrder sort = default(SortOrder), int page = 1)
        {
            var puzzle = puzzleService.GetById(puzzleId);
            if (puzzle == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var datas = answerService.ListAllToPagedAndFilterSort(puzzleId, keyword, sort, page);
            ViewBag.puzzle = puzzle;

            return View(datas);
        }

        public ActionResult Create(Guid puzzleId)
        {
            var puzzle = puzzleService.GetById(puzzleId);
            if (puzzle == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.puzzle = puzzle;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Guid puzzleId, Answer model)
        {
            var puzzle = puzzleService.GetById(puzzleId);
            if (puzzle == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            ViewBag.puzzle = puzzle;

            if (model.Text.IsNullOrWhiteSpace())
            {
                this.SetAlert("答案不可為空！");
                return View(model);
            }

            model.Id = Ci.Sequential.Guid.Create();
            model.CreateTime = DateTime.Now;
            model.ModifyTime = DateTime.Now;
            model.PuzzleId = puzzleId;
            model.Text = model.Text.ToLower().TrimStart(' ').TrimEnd(' ');

            answerService.Create(model);

            this.SetAlert($"答案：{model.Text}，新增成功！");
            return RedirectToAction("Index", "Answer", new { tourId = puzzle.TourId, puzzleId = puzzle.Id });
        }

        public ActionResult Edit(Guid answerId)
        {
            var data = answerService.GetById(answerId);
            if (data == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var puzzle = puzzleService.GetById(data.PuzzleId);
            if (puzzle == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            ViewBag.puzzle = puzzle;

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Guid answerId, Answer model)
        {
            var puzzle = puzzleService.GetById(model.PuzzleId);
            if (puzzle == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (model.Text.IsNullOrWhiteSpace())
            {
                this.SetAlert("答案不可為空！");
                return View(model);
            }

            model.Text = model.Text.ToLower().TrimStart(' ').TrimEnd(' ');
            answerService.Update(model);

            this.SetAlert($"答案：{model.Text}，更新成功！");
            return RedirectToAction("Index", "Answer", new { puzzleId = puzzle.Id });
        }
    }
}