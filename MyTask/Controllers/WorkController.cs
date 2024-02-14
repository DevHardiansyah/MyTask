using MyTask.Models;
using MyTask.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTask.Controllers
{
    public class WorkController : Controller
    {
        DBMytaskEntities db = new DBMytaskEntities();

        [AccessAuthorize]
        public ActionResult Project()
        {
            ViewBag.Title = "Work";
            ViewBag.SubTitle = "Project";

            return View();
        }
        [AccessAuthorize(Roles = "Work_ListTask")]
        public ActionResult TaskList()
        {
            ViewBag.Title = "Work";
            ViewBag.SubTitle = "Task List";

            return View();
        }
        [AccessAuthorize]
        public ActionResult MyTeam()
        {
            ViewBag.Title = "Work";
            ViewBag.SubTitle = "My Team";

            return View();
        }
    }
}