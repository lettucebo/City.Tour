using System;
using System.Collections.Generic;
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
    public class AnswerService : BaseService, IDisposable
    {
        private CityTourEntities db;

        public AnswerService()
        {
            db = new CityTourEntities();
        }

        ~AnswerService()
        {
            Dispose();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public IPagedList<Answer> ListAllToPagedAndFilterSort(Guid puzzleId, string keyword = "", SortOrder sort = null,
            int page = 1, int pageSize = 10)
        {
            var query = db.Answers.Where(x => x.PuzzleId == puzzleId && x.IsDelete == false);

            if (!keyword.IsNullOrWhiteSpace())
            {
                query = query.Where(x => x.Text.Contains(keyword));
            }

            if (sort == null || sort.Key.IsNullOrWhiteSpace())
            {
                sort = new SortOrder() { Key = nameof(Answer.CreateTime), Order = Order.Up };
            }
            query = query.Sort(sort);

            var datas = query.ToPagedList(page, pageSize);
            return datas;
        }

        public void Create(Answer model)
        {
            db.Answers.Add(model);
            db.SaveChanges();
        }

        public Answer GetById(Guid answerId)
        {
            var data = db.Answers.FirstOrDefault(x => x.Id == answerId && x.IsDelete == false);
            return data;
        }

        public void Update(Answer model)
        {
            var data = GetById(model.Id);
            if (data != null)
            {
                data.Text = model.Text;
                data.IsCorrectAnswer = model.IsCorrectAnswer;
                data.ReplyMessage = model.ReplyMessage;

                db.SaveChanges();
            }
        }
    }
}
