#region Usings

using System;
using System.Linq;
using System.Reflection;
using Autofac;
using GoodlyFere.Import.Data;
using GoodlyFere.Import.Data.EntityFramework.Model;
using GoodlyFere.Import.Data.EntityFramework.Repository;
using GoodlyFere.Import.Data.Model;
using Module = Autofac.Module;

#endregion

namespace GoodlyFere.Import.Web.Lib.Autofac
{
    public class DataLayerModule : Module
    {
        #region Methods

        protected override void Load(ContainerBuilder builder)
        {
            var dataAssembly = Assembly.Load(new AssemblyName("GoodlyFere.Import.Data"));
            var efAssembly = Assembly.Load(new AssemblyName("GoodlyFere.Import.Data.EntityFramework"));

            builder.RegisterAssemblyTypes(dataAssembly)
                   .InNamespace("GoodlyFere.Import.Data.NonPersistent.Model")
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(dataAssembly)
                   .InNamespace("GoodlyFere.Import.Data.NonPersistent.Repository")
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(efAssembly)
                   .InNamespace("GoodlyFere.Import.Data.EntityFramework.Model")
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(efAssembly)
                   .InNamespace("GoodlyFere.Import.Data.EntityFramework.Repository")
                   .AsImplementedInterfaces()
                   .SingleInstance();
            
            builder.Register(
                context => new ProjectRepository())
                   .AsImplementedInterfaces()
                   .SingleInstance();

            builder.Register(
                context =>
                new RunConfigRepository())
                   .As(typeof(IWriteRepository<IRunConfiguration>))
                   .SingleInstance();

            builder.Register(
                context =>
                new GenericRepository<ISourceDefinition, SourceDefinition>())
                   .As(typeof(IWriteRepository<ISourceDefinition>))
                   .SingleInstance();

            builder.Register(
                context =>
                new GenericRepository<IDestinationDefinition, DestinationDefinition>())
                   .As(typeof(IWriteRepository<IDestinationDefinition>))
                   .SingleInstance();

            builder.Register(
                context =>
                new GenericRepository<IConverterDefinition, ConverterDefinition>())
                   .As(typeof(IWriteRepository<IConverterDefinition>))
                   .SingleInstance();

            builder.Register(
                context =>
                new GenericRepository<IParameterInstance, ParameterInstance>())
                   .As(typeof(IWriteRepository<IParameterInstance>))
                   .SingleInstance();

            builder.Register(
                context =>
                new GenericRepository<IPlugin, Plugin>())
                   .As(typeof(IWriteRepository<IPlugin>))
                   .SingleInstance();

            base.Load(builder);
        }

        #endregion
    }
}