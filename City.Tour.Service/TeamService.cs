using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using City.Tour.Library.Models.CityTour;
using City.Tour.Service.Base;
using Dapper;

namespace City.Tour.Service
{
    public class TeamService : BaseService, IDisposable
    {
        private CityTourEntities db;

        public TeamService()
        {
            db = new CityTourEntities();
        }

        public TeamService(CityTourEntities ctx)
        {
            db = ctx;
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
            var data = db.Teams.Include(x => x.TeamRecords).Include(x => x.Tour).FirstOrDefault(x => x.Id == teamId);

            if (data == null)
                return null;

            if (!data.CurrentPuzzleId.HasValue)
            {
                data.CurrentPuzzleNum = 1;
                data.CurrentPuzzleId = data.Tour.Puzzle1Id;
            }

            if (!data.TeamRecords.Any())
            {
                // 建立團隊進度紀錄列表紀錄
                var record = new TeamRecord();
                record.Id = Ci.Sequential.Guid.Create();
                record.TeamId = data.Id;
                record.CreateTime = DateTime.Now;
                record.ModifyTime = DateTime.Now;
                data.TeamRecords.Add(record);
            }

            db.SaveChanges();

            return data;
        }

        /// <summary>
        /// 設定隊伍完成路線
        /// </summary>
        /// <param name="teamId"></param>
        public void SetComplete(Guid teamId)
        {
            string sql = "UPDATE Teams SET IsComplete = 1 WHERE TeamId = @teamId";
            using (var conn = new SqlConnection(CityTourConnStr))
            {
                conn.Execute(sql, new { teamId });
            }
        }

        /// <summary>
        /// 設定開始 Tour
        /// </summary>
        /// <param name="teamId"></param>
        public void SetTourStart(Guid teamId)
        {
            var team = GetById(teamId);
            var record = team.TeamRecords.First();
            record.Puzzle1StartTime = DateTime.Now;
            db.SaveChanges();
        }
    }
}
