using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Http;
using System.Collections.Concurrent;

namespace mvc_with_concurrent_dictionary
{
    public class Global : HttpApplication
    {
        public static ConcurrentDictionary<string, Dictionary<string, string>> concurrentDictionary = new ConcurrentDictionary<string, Dictionary<string, string>>();
        void Application_Start(object sender, EventArgs e)
        {
            // 애플리케이션 시작 시 실행되는 코드
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);            
        }
    }
}