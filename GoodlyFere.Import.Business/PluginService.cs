#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Common.Logging;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Business
{
    public class PluginService : GenericService<IPlugin>, IPluginService
    {
        #region Constants and Fields

        private static readonly ILog Log = LogManager.GetLogger<PluginService>();

        #endregion

        #region Constructors and Destructors

        public PluginService(IWriteRepository<IPlugin> repository)
            : base(repository)
        {
        }

        #endregion

        #region Public Methods

        public override void Delete(IEnumerable<IPlugin> entitiesToDelete)
        {
            UnloadAssemblies(entitiesToDelete);
            base.Delete(entitiesToDelete);
        }

        public void LoadAll()
        {
            var plugins = GetAll();
            LoadAssemblies(plugins);
        }

        public bool Reload(IPlugin plugin)
        {
            LoadAssemblies(new[] { plugin });
            return true;
        }

        public override IEnumerable<IPlugin> Save(IEnumerable<IPlugin> newEntities)
        {
            LoadAssemblies(newEntities);
            return base.Save(newEntities);
        }

        public override IEnumerable<IPlugin> Update(IEnumerable<IPlugin> entities)
        {
            LoadAssemblies(entities);
            return base.Update(entities);
        }

        #endregion

        #region Methods

        private void LoadAssemblies(IEnumerable<IPlugin> plugins)
        {
            foreach (var plugin in plugins)
            {
                try
                {
                    string fileName = Path.GetFileName(plugin.AssemblyPath);
                    string binLocation = Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, fileName);
                    File.Copy(plugin.AssemblyPath, binLocation, true);
                    AssemblyName name = AssemblyName.GetAssemblyName(binLocation);
                    Assembly.Load(name);
                }
                catch (ReflectionTypeLoadException rtle)
                {
                    Log.ErrorFormat("Could not load plugin assembly.", rtle);
                    foreach (var loaderEx in rtle.LoaderExceptions)
                    {
                        Log.ErrorFormat("Type load exception: {0}", loaderEx, loaderEx.Message);
                    }
                }
                catch (Exception ex)
                {
                    Log.ErrorFormat("Could not load plugin assembly.", ex);
                }
            }
        }

        private void UnloadAssemblies(IEnumerable<IPlugin> plugins)
        {
            foreach (var plugin in plugins)
            {
                try
                {
                    string fileName = Path.GetFileName(plugin.AssemblyPath);
                    string binLocation = Path.Combine(AppDomain.CurrentDomain.SetupInformation.PrivateBinPath, fileName);
                    File.Delete(binLocation);
                }
                catch (Exception ex)
                {
                    Log.ErrorFormat("Could not unload plugin assembly.", ex);
                }
            }
        }

        #endregion
    }
}