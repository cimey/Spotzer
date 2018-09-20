﻿using Castle.Windsor;
using Castle.Windsor.Installer;
using Spotzer.API.DependencyInjection;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Routing;

namespace Spotzer.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            IWindsorContainer container = new WindsorContainer();
            container.Install(FromAssembly.This());
            var controllerFactory = new WindsorCompositionRoot(container);
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), new WindsorCompositionRoot(container));
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
        }
    }
}
