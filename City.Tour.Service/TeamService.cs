using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using City.Tour.Library.Models.CityTour;
using City.Tour.Service.Base;

namespace City.Tour.Service
{
    public class TeamService : BaseService, IDisposable
    {
        private CityTourEntities db;

        public TeamService()
        {
            db = new CityTourEntities();
        }

        public Team GetByInviteCode(string teamCode)
        {
            var data = db.Teams.FirstOrDefault(x => x.InviteCode == teamCode);
            return data;
        }

        ~TeamService()
        {
            Dispose();
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Team GetById(Guid teamId)
        {
            var data = db.Teams.FirstOrDefault(x => x.Id == teamId);
            return data;
        }
    }
}
