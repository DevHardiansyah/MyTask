
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Security;
using MyTask.Models;
using MyTask.App_Start;

namespace MyTask.Utilities
{
    public class Authentication
    {
        //EmployeeRepository employeeRepo = new EmployeeRepository();
        public DBMytaskEntities db = new DBMytaskEntities();
        public class AuthenticationResult
        {
            public AuthenticationResult(string errorMessage = null)
            {
                ErrorMessage = errorMessage;
            }

            public String ErrorMessage { get; private set; }
            public Boolean IsSuccess => String.IsNullOrEmpty(ErrorMessage);
        }

        private readonly IAuthenticationManager authenticationManager;

        public Authentication(IAuthenticationManager authenticationManager)
        {
            this.authenticationManager = authenticationManager;
        }

        private ClaimsIdentity CreateIdentity(string username, string password)
        {
            //UserBusinessLogic userBusinessLogic = new UserBusinessLogic();
            var groupID = 0;
            var GroupName = "";
            Master_Users emp = new Master_Users();

            emp = db.Master_Users.Where(x => x.Username == username && x.Password == password && x.IsActive == "1").FirstOrDefault();
            var ViewGroupUser = db.ViewGroupUsers.Where(x => x.Username == username).FirstOrDefault();

            if (ViewGroupUser != null)
            {
                groupID = (int)ViewGroupUser.Id;
                GroupName = ViewGroupUser.Role_Name.ToString();
            }


            var identity = new ClaimsIdentity(Startup.MyAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);            
            if (emp != null)
            {
                identity.AddClaim(new Claim(ClaimTypes.Name, emp.Username));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, emp.Username));
                identity.AddClaim(new Claim("UserName", emp.Username));
                identity.AddClaim(new Claim("Email", emp.Username));
                identity.AddClaim(new Claim("FullName", emp.Username));
                identity.AddClaim(new Claim("Password", emp.Password));
                identity.AddClaim(new Claim("EmployeeID", emp.Username));
                identity.AddClaim(new Claim("GroupID", groupID.ToString()));
                identity.AddClaim(new Claim("GroupName", GroupName.ToString()));                
            }

            var groupUser = db.ViewGroupUsers.Where(x => x.Username == username).ToList();

            foreach (var group in groupUser)
            {
                var accessLs = db.ViewGroupMenus.Where(x => x.Id == group.Id).Select(s => s.Url.Replace("/", "_")).ToList();


                if (accessLs.Count > 0)
                {
                    foreach (var access in accessLs)
                    {
                        identity.AddClaim(new Claim(ClaimTypes.Role, access, access, "MyTask"));
                    }
                }
                else
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, "", "MyTask", "MyTask"));
                }
            }

            return identity;
        }

        public AuthenticationResult SignIn(String username, String password)
        {

            var identity = CreateIdentity(username, password);

            authenticationManager.SignOut(Startup.MyAuthentication.ApplicationCookie);
            authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);


            return new AuthenticationResult();
        }
    }
}