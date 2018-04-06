using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
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

            //Puzzle puzzle;
            //switch (team.CurrentPuzzle)
            //{
            //    case 1:
            //        puzzle = puzzleService.GetById(tour.Puzzle1Id.Value);
            //        break;
            //    case 2:
            //        puzzle = puzzleService.GetById(tour.Puzzle2Id.Value);
            //        break;
            //    case 3:
            //        puzzle = puzzleService.GetById(tour.Puzzle3Id.Value);
            //        break;
            //    case 4:
            //        puzzle = puzzleService.GetById(tour.Puzzle4Id.Value);
            //        break;
            //    case 5:
            //        puzzle = puzzleService.GetById(tour.Puzzle5Id.Value);
            //        break;
            //    case 6:
            //        puzzle = puzzleService.GetById(tour.Puzzle6Id.Value);
            //        break;
            //    default:
            //        puzzle = puzzleService.GetById(tour.Puzzle1Id.Value);
            //        break;
            //}

            return View(tour);
        }
    }
}