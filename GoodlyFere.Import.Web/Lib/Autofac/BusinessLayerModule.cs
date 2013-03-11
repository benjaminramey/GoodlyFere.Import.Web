#region Usings

using System;
using System.Linq;
using System.Web;
using Autofac;
using GoodlyFere.Import.Business;
using GoodlyFere.Import.Business.Interfaces;

#endregion

namespace GoodlyFere.Import.Web.Lib.Autofac
{
    public class BusinessLayerModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericService<>)).As(typeof(IGenericService<>));
            builder.RegisterGeneric(typeof(ParametersService<>)).As(typeof(IParametersService<>));
            builder.RegisterType<PluginService>().As<IPluginService>()
                .WithParameter("webSiteRoot", HttpContext.Current.Server.MapPath("~/"));
            builder.RegisterType<RunConfigurationService>().As<IRunConfigurationService>();

            base.Load(builder);
        }

        #endregion
    }
}