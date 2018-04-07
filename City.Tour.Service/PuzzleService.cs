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

        public PuzzleService()
        {
            db = new CityTourEntities();
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
            var data = db.Puzzles.FirstOrDefault(x => x.Id == puzzleId && x.IsDelete == false);
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

        public void SetPassMap(Guid puzzleId)
        {
            var puzzle = GetById(puzzleId);
            puzzle.IsPassMap = true;
            db.SaveChanges();
        }
    }
}
