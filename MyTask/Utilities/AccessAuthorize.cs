using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTask.Utilities
{
    public class AccessAuthorize : AuthorizeAttribute
    {
        /// <summary>
        ///  get user portal
        ///  jika user portal gk null set ke sign in
        /// jika user protal null redirect ke portal
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);



            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!Convert.ToBoolean(ConfigurationManager.AppSettings["isDev"]))
                {
                    try
                    {
                        UserPortal(filterContext);
                    }
                    catch (Exception)
                    {
                        string portalUrl = ConfigurationManager.AppSettings["portalURL"];
                        filterContext.Result = new RedirectResult(portalUrl);
                        return;
                    }
                }
                filterContext.Result = new RedirectResult("~/Account/Login");
            }

            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/Account/Denied");
            }
        }
        private void UserPortal(AuthorizationContext filterContext)
        {
            #region user portal
            var userPortal = new GetUserPortal();
            string userNamePortal = userPortal.GetUser();

            if (!string.IsNullOrEmpty(userNamePortal)) //not null
            {
                IAuthenticationManager authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var authService = new Authentication(authenticationManager);

                var authenticationResult = authService.SignIn(userNamePortal, "");
                if (!authenticationResult.IsSuccess)
                {
                    string portalUrl = ConfigurationManager.AppSettings["portalURL"];
                    filterContext.Result = new RedirectResult(portalUrl);
                    return;

                }
                else if (userNamePortal == "Error : Object reference not set to an instance of an object.")
                {
                    string portalUrl = ConfigurationManager.AppSettings["portalURL"];
                    filterContext.Result = new RedirectResult(portalUrl);
                    return;
                }
                return;
            }

            {

                string portalUrl = ConfigurationManager.AppSettings["portalURL"];
                filterContext.Result = new RedirectResult(portalUrl);
            }


            #endregion
        }
    }
}