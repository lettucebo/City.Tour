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

        /// <summary>
        /// 設定通過謎題地圖
        /// </summary>
        /// <param name="puzzleId"></param>
        /// <param name="teamId"></param>
        public void SetPassMap(Guid puzzleId, Guid teamId)
        {
            var team = teamService.GetByIdIncludeAll(teamId);
            var record = team.TeamRecords.First(x => x.TourPuzzle.PuzzleId == puzzleId);

            record.IsPassPuzzleMap = true;
            record.PassPuzzleMapTime = DateTime.Now;

            db.SaveChanges();
        }

        /// <summary>
        /// 通過答案，設定到下一題
        /// </summary>
        /// <param name="puzzleId"></param>
        /// <param name="teamId"></param>
        public Guid SetNextPuzzle(Guid puzzleId, Guid teamId)
        {
            var team = teamService.GetByIdIncludeAll(teamId);
            var record = team.TeamRecords.First(x => x.TourPuzzle.PuzzleId == puzzleId);
            var tourPuzzles = team.Tour.TourPuzzles.ToList();

            if (record.TourPuzzleId != team.CurrentTourPuzzleId)
            {
                // 代表已經有人觸發往下一關了
                return team.CurrentPuzzleId.Value;
            }

            var nextTourPuzzle = tourPuzzles.FirstOrDefault(x => x.Sort == (team.CurrentTourPuzzleSort + 1));
            if (nextTourPuzzle == null)
            {
                // 沒有下一關，代表已通關
                team.IsComplete = true;
                db.SaveChanges();

                // 已通關
                return Guid.Empty;
            }

            // 設定下一關資訊
            team.CurrentTourPuzzleSort = nextTourPuzzle.Sort;
            team.CurrentPuzzleId = nextTourPuzzle.PuzzleId;
            team.CurrentTourPuzzleId = nextTourPuzzle.Id;
            db.SaveChanges();

            return team.CurrentPuzzleId.Value;
        }
    }
}
