using MyTask.Models;
using MyTask.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTask.Controllers
{
    public class HomeController : Controller
    {
        DBMytaskEntities db = new DBMytaskEntities();

        [AccessAuthorize]
        public ActionResult Index()
        {
            //var accessLs = EmployeeMasterBusinessLogic.GetRoleMenuAccess(AppGlobal.GroupID).Where(s => !s.Url.Contains("Master") && !s.Url.Contains("Configuration")).ToList();
            var accessLs = db.ViewGroupUsers.Where(x => x.Username == AppGlobal.Username && x.Password == AppGlobal.Password).ToList();

            if (accessLs.Count == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            var email = Convert.ToString(((System.Security.Claims.ClaimsPrincipal)System.Web.HttpContext.Current.User).Claims.Where(c => c.Type == "Email").Select(c => c.Value).DefaultIfEmpty("").FirstOrDefault()); ;
            ViewBag.accessLs = accessLs;

            return View();
        }

        [AccessAuthorize(Roles = "Home_About")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [AccessAuthorize]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}