using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace MyTask.Utilities
{
    public class GetUserPortal
    {
        public string GetUser()
        {
            string host = string.Empty;
            string userId = Get_User(System.Web.HttpContext.Current, ref host);
            return userId;
        }

        public static string Get_User(HttpContext p_context, ref string host)
        {
            string result = string.Empty;
            host = string.Empty;

            try
            {
                //host = p_context.Request.UrlReferrer.ToString();
                //host = host.Substring(0, host.IndexOf('/', 9));

                //get cookie fedAuth
                var fedAuth = p_context.Request.Cookies.Get("FedAuth");
                var fedAuthCookie = new Cookie()
                {
                    Expires = fedAuth.Expires,
                    Name = fedAuth.Name,
                    Path = fedAuth.Path,
                    Secure = fedAuth.Secure,
                    Value = String.IsNullOrEmpty(fedAuth.Value) ? "" : fedAuth.Value.Replace(' ', '+')
                };

                var cookies = new List<Cookie>();
                cookies.Add(fedAuthCookie);
                string protocol = p_context.Request.IsSecureConnection ? "https://" : "http://";
                host = protocol + "portal.trakindo.co.id"; // sesuaikan

                string accountId = GetAccountAndId(host, @"application/atom+xml", cookies);
                var usernameFedAuth = accountId.Split('|')[0];

                result = usernameFedAuth;
                
            }
            catch (Exception ex)
            {
                result = "Error : " + ex.Message;
            }

            return result;
        }

        public static string GetAccountAndId(string hostWeb, string acceptHeader, List<Cookie> cookies)
        {
            string requestUrl = hostWeb + "/_api/Web/CurrentUser?$select=Id,LoginName,Title,Email";

            string accountId = string.Empty;

            XDocument docUser = GetXDoc(requestUrl, acceptHeader, cookies);

            //Read properties
            XNamespace ns = "http://www.w3.org/2005/Atom";
            XNamespace d = "http://schemas.microsoft.com/ado/2007/08/dataservices";
            XNamespace m = "http://schemas.microsoft.com/ado/2007/08/dataservices/metadata";

            var userLogName = docUser.Descendants(m + "properties").FirstOrDefault().Element(d + "LoginName").Value;
            var userId = docUser.Descendants(m + "properties").FirstOrDefault().Element(d + "Id").Value;

            accountId = userLogName.Split('|').Last() + "|" + userId;

            return accountId;
        }

        public static XDocument GetXDoc(string requestUrl, string acceptHeader, List<Cookie> cookies)
        {
            var xdoc = new XDocument();

            // prepare HttpWebRequest to execute REST API call
            var httpReq = (HttpWebRequest)WebRequest.Create(requestUrl);

            // add access token string as Authorization header
            httpReq.Accept = acceptHeader;

            string domainCookie = string.Empty;
           

            domainCookie = "trakindo.co.id";

            httpReq.CookieContainer = new CookieContainer();
            foreach (var cookie in cookies)
            {
                cookie.Domain = domainCookie;
                httpReq.CookieContainer.Add(cookie);
            }

            //IgnoreBadCertificates();

            // execute REST API call and inspect response
            HttpWebResponse responseForUser = (HttpWebResponse)httpReq.GetResponse();
            StreamReader readerUser = new StreamReader(responseForUser.GetResponseStream());
            xdoc = XDocument.Load(readerUser);
            readerUser.Close();
            readerUser.Dispose();

            return xdoc;
        }
    }
}