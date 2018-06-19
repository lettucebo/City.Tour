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
    public class TourService : BaseService
    {
        private CityTourEntities db;

        public TourService()
        {
            db = new CityTourEntities();
        }

        public TourService(CityTourEntities ctx)
        {
            db = ctx;
        }

        public IPagedList<Library.Models.CityTour.Tour> ListAllToPagedAndFilterSort(string keyword = "", SortOrder sort = null, int page = 1, int pageSize = 10)
        {
            var query = db.Tours.Where(x => x.IsDelete == false);

            if (!keyword.IsNullOrWhiteSpace())
            {
                query = query.Where(x =>
                    x.Name.Contains(keyword) || x.Info.Contains(keyword) || x.Descript.Contains(keyword));
            }

            if (sort == null || sort.Key.IsNullOrWhiteSpace())
            {
                sort = new SortOrder() { Key = nameof(Library.Models.CityTour.Tour.CreateTime), Order = Order.Down };
            }
            query = query.Sort(sort);

            var datas = query.ToPagedList(page, pageSize);
            return datas;
        }

        public Library.Models.CityTour.Tour GetById(Guid tourId)
        {
            var data = db.Tours.Include(x => x.TourPuzzles).FirstOrDefault(x => x.IsDelete == false);
            return data;
        }
    }
}
