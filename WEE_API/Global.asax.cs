using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WEE_API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            CultureInfo cInf = new CultureInfo("vi-VN", false)
            {
                DateTimeFormat =
                {
                    DateSeparator = "/",
                    ShortDatePattern = "dd/MM/yyyy",
                    LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
                }
            };
            Thread.CurrentThread.CurrentCulture = cInf;
            Thread.CurrentThread.CurrentUICulture = cInf;
        }
    }
}
