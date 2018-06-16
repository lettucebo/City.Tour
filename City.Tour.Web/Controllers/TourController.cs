using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
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



            ViewBag.currentPuzzle = team.CurrentPuzzleId;

            return View(tour);
        }

        public ActionResult PuzzleMap(Guid puzzleId)
        {
            var puzzle = puzzleService.GetById(puzzleId);
            if (puzzle == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "找不到 puzzle");

            var teamId = new Guid(((ClaimsPrincipal)User).FindFirst("TeamId").Value ?? Guid.Empty.ToString());
            var team = teamService.GetById(teamId);
            var tour = team.Tour;
            var record = team.TeamRecords.First();

            if (puzzle.Id == tour.Puzzle1Id && !record.Puzzle1StartTime.HasValue)
            {
                // 設定 puzzle 1 開始時間
                teamService.SetTourStart(teamId);

            }

            // 檢查是否已經為目前最新進度，若非則跳轉
            if (team.CurrentPuzzleId != puzzle.Id)
            {
                return RedirectToAction("PuzzleMap", "Tour", new { puzzleId });
            }

            if (team.IsComplete)
            {
                return RedirectToAction("Complete", "Tour");
            }

            // 判斷是否已經通過地圖
            if (puzzleId == tour.Puzzle1Id && record.IsPassPuzzle1Map)
            {
                return RedirectToAction("Puzzle", "Tour", new { puzzleId });
            }
            else if (puzzleId == tour.Puzzle2Id && record.IsPassPuzzle2Map)
            {
                return RedirectToAction("Puzzle", "Tour", new { puzzleId });
            }
            else if (puzzleId == tour.Puzzle3Id && record.IsPassPuzzle3Map)
            {
                return RedirectToAction("Puzzle", "Tour", new { puzzleId });
            }
            else if (puzzleId == tour.Puzzle4Id && record.IsPassPuzzle4Map)
            {
                return RedirectToAction("Puzzle", "Tour", new { puzzleId });
            }
            else if (puzzleId == tour.Puzzle5Id && record.IsPassPuzzle5Map)
            {
                return RedirectToAction("Puzzle", "Tour", new { puzzleId });
            }
            else if (puzzleId == tour.Puzzle6Id && record.IsPassPuzzle6Map)
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

            var teamId = new Guid(((ClaimsPrincipal)User).FindFirst("TeamId").Value ?? Guid.Empty.ToString());

            if (!answer.IsNullOrWhiteSpace() && answer.ToUpper().Trim() == puzzle.MapAnswer.ToUpper().Trim())
            {
                puzzleService.SetPassMap(puzzleId, teamId);
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

            var teamId = new Guid(((ClaimsPrincipal)User).FindFirst("TeamId").Value ?? Guid.Empty.ToString());
            var team = teamService.GetById(teamId);
            var tour = team.Tour;
            var record = team.TeamRecords.First();

            // 檢查是否已經為目前最新進度，若非則跳轉
            if (team.CurrentPuzzleId != puzzle.Id)
            {
                return RedirectToAction("PuzzleMap", "Tour", new { puzzleId });
            }

            if (team.IsComplete)
            {
                return RedirectToAction("Complete", "Tour");
            }

            // 判斷是否已經通過地圖
            if (puzzleId == tour.Puzzle1Id && !record.IsPassPuzzle1Map)
            {
                return RedirectToAction("PuzzleMap", "Tour", new { puzzleId });
            }
            else if (puzzleId == tour.Puzzle2Id && !record.IsPassPuzzle2Map)
            {
                return RedirectToAction("PuzzleMap", "Tour", new { puzzleId });
            }
            else if (puzzleId == tour.Puzzle3Id && !record.IsPassPuzzle3Map)
            {
                return RedirectToAction("PuzzleMap", "Tour", new { puzzleId });
            }
            else if (puzzleId == tour.Puzzle4Id && !record.IsPassPuzzle4Map)
            {
                return RedirectToAction("PuzzleMap", "Tour", new { puzzleId });
            }
            else if (puzzleId == tour.Puzzle5Id && !record.IsPassPuzzle5Map)
            {
                return RedirectToAction("PuzzleMap", "Tour", new { puzzleId });
            }
            else if (puzzleId == tour.Puzzle6Id && !record.IsPassPuzzle6Map)
            {
                return RedirectToAction("PuzzleMap", "Tour", new { puzzleId });
            }

            ViewBag.hints = puzzle.Hints.ToArray();

            return View(puzzle);
        }


        [HttpPost]
        public ActionResult Puzzle(Guid puzzleId, string answer)
        {
            var puzzle = puzzleService.GetById(puzzleId);
            if (puzzle == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "找不到 puzzle");

            var teamId = new Guid(((ClaimsPrincipal)User).FindFirst("TeamId").Value ?? Guid.Empty.ToString());

            if (!answer.IsNullOrWhiteSpace() && answer.ToUpper().Trim() == puzzle.Answer.ToUpper().Trim())
            {
                var nextPuzzleId = puzzleService.SetNextPuzzle(puzzleId, teamId);
                if (nextPuzzleId == Guid.Empty)
                {
                    // 代表已通關
                    return RedirectToAction("Complete", "Tour");
                }

                this.SetAlert("答案正確！");
                return RedirectToAction("PuzzleMap", "Tour", new { puzzleId = nextPuzzleId });
            }

            this.SetAlert("答案錯誤，請再試一次！");
            return View(puzzle);
        }

        public ActionResult Complete(Guid teamId)
        {
            teamService.SetComplete(teamId);
            return View();
        }
    }
}