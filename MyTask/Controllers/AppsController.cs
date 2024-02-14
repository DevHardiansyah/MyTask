using MyTask.Models;
using MyTask.Utilities;
using System.Web.Mvc;
using MyTask.BusinessLogic;
using MyTask.BusinessLogic.AppsBusinessLogic;

namespace MyTask.Controllers
{
    public class AppsController : Controller
    {
        DBMytaskEntities db = new DBMytaskEntities();

        [AccessAuthorize]
        public ActionResult Calendar()
        {
            ViewBag.Title = "Apps";
            ViewBag.SubTitle = "Calendar";
            
            return View();
        }
        [AccessAuthorize]
        public new ActionResult Profile()
        {
            ViewBag.Title = "Apps";
            ViewBag.SubTitle = "Profile";

            ProfileBusinessLogic pbl = new ProfileBusinessLogic();

            var getusername = AppGlobal.Username;
            var DataUser = pbl.fnGetuserbyUsername(getusername);


            ViewBag.GetUser = DataUser;



            return View();
        }
    }
}