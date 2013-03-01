#region Usings

using System;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using GoodlyFere.Import.Web.Lib;
using GoodlyFere.Import.Web.Lib.Autofac;
using Newtonsoft.Json.Serialization;

#endregion

namespace GoodlyFere.Import.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        #region Methods

        protected void Application_Start()
        {
            IoCSetup();
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        private void IoCSetup()
        {
            var configuration = GlobalConfiguration.Configuration;
            var webAssembly = typeof(MvcApplication).Assembly;
            var builder = new ContainerBuilder();

            configuration.Formatters.Remove(configuration.Formatters.JsonFormatter);
            configuration.Formatters.Add(new IoCJsonMediatTypeFormatter());

            builder.RegisterModule(new BusinessLayerModule());
            builder.RegisterModule(new DataLayerModule());
            builder.RegisterControllers(webAssembly);
            builder.RegisterApiControllers(webAssembly);
            builder.RegisterWebApiFilterProvider(configuration);
            AppContext.Container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(AppContext.Container));
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(AppContext.Container);
        }

        #endregion
    }
}