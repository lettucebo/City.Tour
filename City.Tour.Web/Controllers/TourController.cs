using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ci.Extension;
using Ci.Mvc.Alert;
using City.Tour.Library.Models.CityTour;
using City.Tour.Service;
using City.Tour.Web.Base;

namespace City.Tour.Web.Controllers
{
    public class TourController : BaseController
    {
        private TourService tourService = new TourService();
        private PuzzleService puzzleService = new PuzzleService();
        private TeamService teamService = new TeamService();

        public ActionResult Index(Guid teamId)
        {
            var team = teamService.GetById(teamId);

            var tour = tourService.GetById(team.TourId);
            if (tour == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "找不到 Tour");

            ViewBag.currentPuzzle = team.CurrentPuzzle ?? tour.Puzzle1Id;

            return View(tour);
        }

        public ActionResult PuzzleMap(Guid puzzleId)
        {
            var puzzle = puzzleService.GetById(puzzleId);
            if (puzzle == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "找不到 puzzle");

            if (puzzle.IsPassMap)
            {
                return RedirectToAction("Puzzle", "Tour", new { puzzleId });
            }

            return View(puzzle);
        }

        [HttpPost]
        public ActionResult PuzzleMap(Guid puzzleId, string answer)
        {
            var puzzle = puzzleService.GetById(puzzleId);
            if (puzzle == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "找不到 puzzle");

            if (!answer.IsNullOrWhiteSpace() && answer.Trim() == puzzle.MapAnswer.Trim())
            {
                puzzleService.SetPassMap(puzzleId);
                return RedirectToAction("Puzzle", "Tour", new { puzzleId });
            }

            this.SetAlert("答案錯誤，請再試一次！");
            return View(puzzle);
        }

        public ActionResult Puzzle(Guid puzzleId)
        {
            var puzzle = puzzleService.GetById(puzzleId);
            if (puzzle == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "找不到 puzzle");

            if (!puzzle.IsPassMap)
                return RedirectToAction("PuzzleMap", "Tour", new { puzzleId });

            return View(puzzle);
        }
    }
}