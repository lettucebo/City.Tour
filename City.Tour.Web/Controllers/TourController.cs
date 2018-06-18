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
            var team = teamService.GetByIdIncludeAll(teamId);

            var tour = team.Tour;
            if (tour == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "找不到 Tour");
            if (team.TeamRecords.Any())
            {
                ViewBag.currentPuzzleId = team.TeamRecords.OrderByDescending(x => x.Sort).First().TourPuzzle.PuzzleId;
            }
            else
            {
                ViewBag.currentPuzzleId = team.Tour.TourPuzzles.OrderBy(x => x.Sort).First().PuzzleId;
            }

            return View(tour);
        }

        public ActionResult PuzzleMap(Guid puzzleId)
        {
            var puzzle = puzzleService.GetById(puzzleId);
            if (puzzle == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "找不到 puzzle");

            var teamId = new Guid(((ClaimsPrincipal)User).FindFirst("TeamId").Value ?? Guid.Empty.ToString());
            var team = teamService.GetByIdIncludeAll(teamId);
            var teamRecord = team.TeamRecords;

            // 檢查是否已經有任何紀錄，若沒有則代表全新開始，寫入第一題記錄與開始時間
            if (!teamRecord.Any())
            {
                teamService.SetTeamStart(teamId);
            }

            // 檢查是否已經為目前最新進度，若非則跳轉
            if (team.CurrentPuzzleId != puzzle.Id)
            {
                return RedirectToAction("PuzzleMap", "Tour", new { puzzleId = team.CurrentPuzzleId });
            }

            if (team.IsComplete)
            {
                return RedirectToAction("Complete", "Tour", new { teamId });
            }

            // 判斷是否已經通過地圖
            var record = team.TeamRecords.OrderByDescending(x => x.Sort).First(x => x.TourPuzzle.PuzzleId == puzzle.Id);
            if (record.IsPassPuzzleMap)
            {
                return RedirectToAction("Puzzle", "Tour", new { puzzleId = record.TourPuzzle.PuzzleId });
            }

            // todo 設定到期時間

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

            // todo 設定到期時間

            this.SetAlert("答案錯誤，請再試一次！");
            return View(puzzle);
        }

        public ActionResult Puzzle(Guid puzzleId)
        {
            var puzzle = puzzleService.GetById(puzzleId);
            if (puzzle == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "找不到 puzzle");

            var teamId = new Guid(((ClaimsPrincipal)User).FindFirst("TeamId").Value ?? Guid.Empty.ToString());
            var team = teamService.GetByIdIncludeAll(teamId);


            // 檢查是否已經為目前最新進度，若非則跳轉
            if (team.CurrentPuzzleId != puzzle.Id)
            {
                return RedirectToAction("PuzzleMap", "Tour", new { puzzleId });
            }

            if (team.IsComplete)
            {
                return RedirectToAction("Complete", "Tour", new { teamId });
            }

            // 判斷是否已經通過地圖
            var record = team.TeamRecords.OrderByDescending(x => x.Sort).First(x => x.TourPuzzle.PuzzleId == puzzle.Id);
            if (!record.IsPassPuzzleMap)
            {
                return RedirectToAction("PuzzleMap", "Tour", new { puzzleId = record.TourPuzzle.PuzzleId });
            }

            // 設定提示
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
                // 設定前往下一關
                var nextPuzzleId = puzzleService.SetNextPuzzle(puzzleId, teamId);
                if (nextPuzzleId == Guid.Empty)
                {
                    // 代表已通此關，前往下一關
                    return RedirectToAction("Complete", "Tour", new { teamId });
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