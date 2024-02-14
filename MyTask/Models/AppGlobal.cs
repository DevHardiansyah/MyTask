using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace MyTask
{
    public class AppGlobal
    {
        public static string Username
        {
            get
            {
                try
                {
                    return Convert.ToString(((System.Security.Claims.ClaimsPrincipal)HttpContext.Current.User).Claims.Where(c => c.Type == "UserName").Select(c => c.Value).DefaultIfEmpty("").FirstOrDefault());
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public static string Password
        {
            get
            {
                try
                {
                    return Convert.ToString(((System.Security.Claims.ClaimsPrincipal)HttpContext.Current.User).Claims.Where(c => c.Type == "Password").Select(c => c.Value).DefaultIfEmpty("").FirstOrDefault());
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public static int GroupID
        {
            get
            {
                try
                {
                    return Convert.ToInt32(((System.Security.Claims.ClaimsPrincipal)HttpContext.Current.User).Claims.Where(c => c.Type == "GroupID").Select(c => c.Value).DefaultIfEmpty("").FirstOrDefault());
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public static string GroupName
        {
            get
            {
                try
                {
                    return ((System.Security.Claims.ClaimsPrincipal)HttpContext.Current.User).Claims.Where(c => c.Type == "GroupName").Select(c => c.Value).DefaultIfEmpty("").FirstOrDefault();
                }
                catch (Exception)
                {
                    return "";
                }
            }
        }

        public static bool UserIsPricingAdmin
        {
            get
            {
                try
                {
                    return GroupName == "MSIS_PricingAdmin" || GroupName == "MSIS_PricingSuperAdmin";
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static string Fullname
        {
            get
            {
                try
                {
                    return Convert.ToString(((System.Security.Claims.ClaimsPrincipal)HttpContext.Current.User).Claims.Where(c => c.Type == "FullName").Select(c => c.Value).DefaultIfEmpty("").FirstOrDefault());
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public static string EmployeeID
        {
            get
            {
                try
                {
                    return Convert.ToString(((System.Security.Claims.ClaimsPrincipal)HttpContext.Current.User).Claims.Where(c => c.Type == "EmployeeID").Select(c => c.Value).DefaultIfEmpty("").FirstOrDefault());
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public static long EmployeeIDLong
        {
            get
            {
                try
                {
                    return Convert.ToInt64(((System.Security.Claims.ClaimsPrincipal)HttpContext.Current.User).Claims.Where(c => c.Type == "EmployeeID").Select(c => c.Value).DefaultIfEmpty("").FirstOrDefault());
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public static string Phantomjspath
        {
            get
            {
                try
                {
                    string path = ConfigurationManager.AppSettings["Phantomjspath"].ToString();
                    return HttpContext.Current.Server.MapPath(path);

                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public static string PathAttachmentFG
        {
            get
            {
                try
                {
                    string path = ConfigurationManager.AppSettings["PathAttachmentFG"].ToString();
                    ////return path;
                    return HttpContext.Current.Server.MapPath(path);

                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }
        }

        public static void CreateLog(string type, string message)
        {
            var PATH_LOG = ConfigurationManager.AppSettings["PATH_LOG"];
            var FileNameLog = ConfigurationManager.AppSettings["FileNameLog"];
            string path = System.IO.Directory.GetCurrentDirectory().Replace("\\bin\\Debug", "") + PATH_LOG;


            message = DateTime.Now + " [" + type + "] " + message;

            if (Directory.Exists(path))
            {
                if (!File.Exists(path + FileNameLog))
                    File.Create(path + FileNameLog);

                File.AppendAllText(path + FileNameLog, message + Environment.NewLine);
            }
        }
    }
}