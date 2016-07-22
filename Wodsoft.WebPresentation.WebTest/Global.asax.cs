﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Wodsoft.Web.Mvc;

namespace Wodsoft.Web.WebTest
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Assembly.Load("Wodsoft.WebPresentation.Core");
            Assembly.Load("Wodsoft.WebPresentation");
            Assembly.Load("Wodsoft.WebPresentation.Visualization");

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            Xaml.XamlReader reader = new Xaml.XamlReader();
            reader.Load(Server.MapPath("Global.xaml"));

            ViewEngines.Engines.Insert(0, new WebPresentationViewEngine());
        }
    }
}