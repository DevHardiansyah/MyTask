using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MyTask.Models;
namespace MyTask
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var interval = ConfigurationManager.AppSettings["SCHEDULER_TIME"];
            
            if (!string.IsNullOrWhiteSpace(interval) && Convert.ToBoolean(ConfigurationManager.AppSettings["SCHEDULER"]))
            {

                JobManager.Initialize();
                //JobManager.JobException += info => LogHelper.LogError(info.Exception, "An error just happened with a scheduled job: ");

                //JobManager.JobStart += info => LogHelper.LogInformation($"{info.Name}: started");

                //JobManager.JobEnd += info => LogHelper.LogInformation($"{info.Name}: ended ({info.Duration})");

                JobManager.AddJob(() => new EmailScheduler(),
                    s => s.NonReentrant().ToRunEvery(Convert.ToInt32(interval)).Minutes()
                );

            }
        }
    }
}
