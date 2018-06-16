using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ci.Extension;
using Ci.Mvc.Sort;
using Ci.Mvc.Sort.Enums;
using Ci.Mvc.Sort.Models;
using City.Tour.Library.Models.CityTour;
using City.Tour.Service.Base;
using X.PagedList;

namespace City.Tour.Service
{
    public class PuzzleService : BaseService, IDisposable
    {
        private CityTourEntities db;
        private TeamService teamService;
        private TourService tourService;

        public PuzzleService()
        {
            db = new CityTourEntities();
            teamService = new TeamService(db);
            tourService = new TourService(db);
        }

        public IPagedList<Puzzle> ListAllToPagedAndFilterSort(Guid tourId, string keyword = "", SortOrder sort = null, int page = 1, int pageSize = 10)
        {
            var query = db.Puzzles.Where(x => x.TourId == tourId && x.IsDelete == false);

            if (!keyword.IsNullOrWhiteSpace())
            {
                query = query.Where(x =>
                    x.Name.Contains(keyword) || x.Descript.Contains(keyword));
            }

            if (sort == null || sort.Key.IsNullOrWhiteSpace())
            {
                sort = new SortOrder() { Key = nameof(Puzzle.Sort), Order = Order.Up };
            }
            query = query.Sort(sort);

            var datas = query.ToPagedList(page, pageSize);
            return datas;
        }

        public void Create(Puzzle model)
        {
            if (model == null)
                return;

            db.Puzzles.Add(model);
            db.SaveChanges();
        }

        public Puzzle GetById(Guid puzzleId)
        {
            var data = db.Puzzles.Include(x => x.Hints).FirstOrDefault(x => x.Id == puzzleId && x.IsDelete == false);
            return data;
        }

        public void Update(Puzzle model)
        {
            var data = GetById(model.Id);
            if (data != null)
            {
                data.Name = model.Name;
                data.Sort = model.Sort;
                data.Descript = model.Descript;

                if (!model.Picture.IsNullOrWhiteSpace())
                    data.Picture = model.Picture;

                data.ModifyTime = DateTime.Now;

                db.SaveChanges();
            }
        }

        ~PuzzleService()
        {
            Dispose();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public void SetPassMap(Guid puzzleId, Guid teamId)
        {
            var puzzle = GetById(puzzleId);
            var team = teamService.GetById(teamId);
            var record = team.TeamRecords.First();
            var tour = team.Tour;

            if (puzzle.Id == tour.Puzzle1Id)
            {
                record.IsPassPuzzle1Map = true;
                record.PassPuzzle1MapTime = DateTime.Now;
            }
            else if (puzzle.Id == tour.Puzzle2Id)
            {
                record.IsPassPuzzle2Map = true;
                record.PassPuzzle2MapTime = DateTime.Now;
            }
            else if (puzzle.Id == tour.Puzzle3Id)
            {
                record.IsPassPuzzle3Map = true;
                record.PassPuzzle3MapTime = DateTime.Now;
            }
            else if (puzzle.Id == tour.Puzzle4Id)
            {
                record.IsPassPuzzle4Map = true;
                record.PassPuzzle4MapTime = DateTime.Now;
            }
            else if (puzzle.Id == tour.Puzzle5Id)
            {
                record.IsPassPuzzle5Map = true;
                record.PassPuzzle5MapTime = DateTime.Now;
            }
            else if (puzzle.Id == tour.Puzzle6Id)
            {
                record.IsPassPuzzle6Map = true;
                record.PassPuzzle6MapTime = DateTime.Now;
            }

            db.SaveChanges();
        }

        /// <summary>
        /// 通過答案，設定到下一題
        /// </summary>
        /// <param name="puzzleId"></param>
        /// <param name="teamId"></param>
        public Guid SetNextPuzzle(Guid puzzleId, Guid teamId)
        {
            var team = teamService.GetById(teamId);
            var currentPuzzleNum = team.CurrentPuzzleNum;
            var currentPuzzleId = team.CurrentPuzzleId;
            var tour = tourService.GetById(team.TourId);
            var record = team.TeamRecords.First();

            if (currentPuzzleId.HasValue && puzzleId != currentPuzzleId)
            {
                // 代表已經有人觸發往下一關了
                return currentPuzzleId.Value;
            }

            // 找出下一關的 Id，寫死邏輯
            if (!currentPuzzleId.HasValue || currentPuzzleId.Value == tour.Puzzle1Id)
            {
                if (tour.Puzzle2Id.HasValue)
                {
                    record.Puzzle1CompleteTime = DateTime.Now;
                    record.Puzzle1StartTime = DateTime.Now;
                    team.CurrentPuzzleId = tour.Puzzle2Id.Value;
                    team.CurrentPuzzleNum = 2;
                    db.SaveChanges();

                    return tour.Puzzle2Id.Value;
                }

                // 沒有值，代表沒下一關
                return Guid.Empty;
            }
            else if (currentPuzzleId.Value == tour.Puzzle2Id)
            {
                if (tour.Puzzle3Id.HasValue)
                {
                    record.Puzzle2CompleteTime = DateTime.Now;
                    record.Puzzle3StartTime = DateTime.Now;
                    team.CurrentPuzzleId = tour.Puzzle3Id.Value;
                    team.CurrentPuzzleNum = 3;
                    db.SaveChanges();

                    return tour.Puzzle3Id.Value;
                }

                // 沒有值，代表沒下一關
                return Guid.Empty;
            }
            else if (currentPuzzleId.Value == tour.Puzzle3Id)
            {
                if (tour.Puzzle4Id.HasValue)
                {
                    record.Puzzle3CompleteTime = DateTime.Now;
                    record.Puzzle4StartTime = DateTime.Now;
                    team.CurrentPuzzleId = tour.Puzzle4Id.Value;
                    team.CurrentPuzzleNum = 4;
                    db.SaveChanges();

                    return tour.Puzzle4Id.Value;
                }

                // 沒有值，代表沒下一關
                return Guid.Empty;
            }
            else if (currentPuzzleId.Value == tour.Puzzle4Id)
            {
                if (tour.Puzzle5Id.HasValue)
                {
                    record.Puzzle4CompleteTime = DateTime.Now;
                    record.Puzzle5StartTime = DateTime.Now;
                    team.CurrentPuzzleId = tour.Puzzle5Id.Value;
                    team.CurrentPuzzleNum = 5;
                    db.SaveChanges();

                    return tour.Puzzle5Id.Value;
                }

                // 沒有值，代表沒下一關
                return Guid.Empty;
            }
            else if (currentPuzzleId.Value == tour.Puzzle5Id)
            {
                if (tour.Puzzle6Id.HasValue)
                {
                    record.Puzzle5CompleteTime = DateTime.Now;
                    record.Puzzle6StartTime = DateTime.Now;
                    team.CurrentPuzzleId = tour.Puzzle6Id.Value;
                    team.CurrentPuzzleNum = 6;
                    db.SaveChanges();

                    return tour.Puzzle6Id.Value;
                }

                // 沒有值，代表沒下一關
                return Guid.Empty;
            }
            else
            {
                record.Puzzle6CompleteTime = DateTime.Now;
                team.IsComplete = true;
                db.SaveChanges();

                // 已通關
                return Guid.Empty;
            }
        }
    }
}
