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

        /// <summary>
        /// 根據 Id 取得團隊資訊
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public Team GetByIdIncludeAll(Guid teamId, bool isNoTracking = false)
        {
            var query = db.Teams
                .Include(x => x.TeamRecords)
                .Include(x => x.Tour)
                .Include(x => x.Tour.TourPuzzles)
                .Include(x => x.TeamRecords.Select(y => y.TourPuzzle))
                .Where(x => x.Id == teamId);

            if (isNoTracking)
            {
                query = query.AsNoTracking();
            }

            var data = query.FirstOrDefault();

            return data;
        }

        /// <summary>
        /// 設定隊伍完成路線
        /// </summary>
        /// <param name="teamId"></param>
        public void SetComplete(Guid teamId)
        {
            string sql = "UPDATE Teams SET IsComplete = 1 WHERE Id = @teamId";
            using (var conn = new SqlConnection(CityTourConnStr))
            {
                conn.Execute(sql, new { teamId });
            }
        }

        /// <summary>
        /// 設定開始 Tour 計時
        /// </summary>
        /// <param name="teamId"></param>
        public Guid SetTeamStart(Guid teamId)
        {
            var team = GetByIdIncludeAll(teamId);
            var tourPuzzles = team.Tour.TourPuzzles;
            var records = team.TeamRecords;
            if (team.CurrentPuzzleId.HasValue && records.Any())
                return team.CurrentPuzzleId.Value;

            if (!team.CurrentPuzzleId.HasValue)
            {
                var firstTourPuzzle = tourPuzzles.OrderBy(x => x.Sort).First();
                var record = new TeamRecord();
                record.Id = Ci.Sequential.Guid.Create();
                record.TeamId = team.Id;
                record.CreateTime = DateTime.Now;
                record.ModifyTime = DateTime.Now;
                record.PuzzleStartTime = DateTime.Now;
                record.TourPuzzleId = firstTourPuzzle.Id;
                record.Sort = firstTourPuzzle.Sort;
                team.TeamRecords.Add(record);

                team.CurrentPuzzleId = tourPuzzles.First(x => x.Sort == 1).PuzzleId;
                team.CurrentTourPuzzleId = firstTourPuzzle.Id;
                team.CurrentTourPuzzleSort = firstTourPuzzle.Sort;
            }
            else
            {
                team.CurrentPuzzleId = team.TeamRecords.OrderByDescending(x => x.Sort).First().TourPuzzle.PuzzleId;
            }

            db.SaveChanges();

            return team.CurrentPuzzleId.Value;
        }

        public void DeleteTeamRecordByTeamCode(string teamCode)
        {
            var team = GetByInviteCode(teamCode);
            if (team == null)
                return;
            string sql = @"
                            DELETE FROM TeamRecords WHERE TeamId = @teamId;
                            UPDATE Teams SET [CurrentTourPuzzleSort] = 1, [CurrentPuzzleId] = NULL, [CurrentTourPuzzleId] = NULL WHERE Id = @teamId;
                            ";

            using (var conn = new SqlConnection(CityTourConnStr))
            {
                conn.Execute(sql, new { teamId = team.Id });
            }
        }

        public void DeleteTeamMemberByTeamCode(string teamCode)
        {
            var team = GetByInviteCode(teamCode);
            if (team == null)
                return;
            string sql = @"DELETE FROM Users WHERE TeamId = @teamId;";

            using (var conn = new SqlConnection(CityTourConnStr))
            {
                conn.Execute(sql, new { teamId = team.Id });
            }
        }
    }
}
