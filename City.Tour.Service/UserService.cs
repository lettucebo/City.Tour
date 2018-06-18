using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using City.Tour.Library.Models.CityTour;
using City.Tour.Service.Base;
using DateTime = System.DateTime;

namespace City.Tour.Service
{
    public class UserService : BaseService
    {
        private CityTourEntities db;

        public UserService()
        {
            db = new CityTourEntities();
        }

        public User CheckOrCreate(User model)
        {
            var data = db.Users.Include(x => x.Team).FirstOrDefault(x => x.ProfileId == model.ProfileId);
            if (data == null)
            {
                data = model;
                data.Id = Ci.Sequential.Guid.Create();
                data.CreateTime = DateTime.Now;
                data.ModifyTime = DateTime.Now;
                data.Picture = $"https://graph.facebook.com/{model.ProfileId}/picture?type=large";
                db.Users.Add(data);
            }

            db.SaveChanges();

            return data;
        }
    }
}
