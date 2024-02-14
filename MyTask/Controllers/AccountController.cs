using Microsoft.Owin.Security;
using System;
using MyTask.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using System.Threading.Tasks;
using MyTask.Models;
using static MyTask.Models.AccountViewModel;
using System.Configuration;

namespace MyTask.Controllers
{
    public class AccountController : Controller
    {
        readonly DBMytaskEntities _db = new DBMytaskEntities();
        // GET: Account
        [AccessAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            Boolean isDev = Convert.ToBoolean(WebConfigurationManager.AppSettings["isDev"]);

            if (!isDev)
            {
                IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                var authService = new Authentication(authenticationManager);
                var userPortal = new GetUserPortal();

                string userNamePortal = userPortal.GetUser();

                var authenticationResult = authService.SignIn(userNamePortal, "");

                string username = User.Identity.Name;

                if (authenticationResult.IsSuccess)
                {

                    // we are in! 
                    return RedirectToLocal(returnUrl);

                }
            }
            return View();
        }
        
        [HttpPost]       
        [AllowAnonymous]        
        public JsonResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return Json("Error", JsonRequestBehavior.AllowGet);
            }

            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            var authService = new Authentication(authenticationManager);

            var authenticationResult = authService.SignIn(model.Username, model.Password);

            string username = "";
            UserBusinessLogic bc = new UserBusinessLogic();
            username = bc.GetUser(model).Username;            

            if (authenticationResult.IsSuccess && username != "")
            {
                // we are in!                 
                return Json("Success", JsonRequestBehavior.AllowGet);

            }
            ViewBag.ReturnUrl = returnUrl;

            return Json("Error", JsonRequestBehavior.AllowGet);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut(Startup.MyAuthentication.ApplicationCookie);
            Session.Abandon();
            Session.Clear();


            if (!Convert.ToBoolean(ConfigurationManager.AppSettings["isDev"]))
            {
                string portalURL = System.Configuration.ConfigurationManager.AppSettings["portalURL"];
                return Redirect(portalURL);
            }
            return RedirectToAction("Login", "Account");

        }
        public ActionResult Denied()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginMail()
        {
            return View();
        }
        
        public JsonResult CekMail(LoginViewModel model, string returnurl)
        {           

            string mail = null;
            var data = _db.Master_Users.FirstOrDefault(x => x.Username == model.Username);

            if(data != null)
            {
                mail = data.Username;
            }
            else
            {
                mail = "error";
            }

            Session["Mail"] = mail;

            return Json(mail,JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult SuccessSend()
        {
            return View();
        }

    }
}