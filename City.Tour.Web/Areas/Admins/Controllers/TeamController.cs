using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using City.Tour.Service;

namespace City.Tour.Web.Areas.Admins.Controllers
{
    public class TeamController : Controller
    {
        private TeamService teamService = new TeamService();

        public ActionResult ClearTeamRecord(string teamCode)
        {
            teamService.DeleteTeamRecordByTeamCode(teamCode);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public ActionResult ClearTeamMember(string teamCode)
        {
            teamService.DeleteTeamMemberByTeamCode(teamCode);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}