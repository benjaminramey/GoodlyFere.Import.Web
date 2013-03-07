#region Usings

using System;
using System.Linq;
using System.Reflection;
using Common.Logging;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Data.NonPersistent.Repository
{
    public abstract class TypeCacheRepository<TAvailableType, TInterface>
        where TAvailableType : IImporterTypeModel, new()
    {
        #region Constants and Fields

        private static readonly ILog Log = LogManager.GetLogger(typeof(TypeCacheRepository<,>));
        private static TAvailableType[] _cache;

        #endregion

        #region Constructors and Destructors

        protected TypeCacheRepository()
        {
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
        }

        #endregion

        #region Properties

        protected TAvailableType[] Cache
        {
            get
            {
                return _cache ?? (_cache = GetCache());
            }
        }

        #endregion

        #region Methods

        private void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            _cache = null;
        }

        private TAvailableType[] GetCache()
        {
            try
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(a => a.FullName.StartsWith("GoodlyFere.Import")
                                || a.GetReferencedAssemblies().Any(ra => ra.Name.StartsWith("GoodlyFere.Import")))
                    .ToArray();

                return assemblies.SelectMany(a => a.DefinedTypes)
                                 .Where(
                                     t => typeof(TInterface).IsAssignableFrom(t)
                                          && !t.IsInterface
                                          && !t.IsAbstract)
                                 .Select(t => new TAvailableType { FullTypeName = t.AssemblyQualifiedName })
                                 .ToArray();
            }
            catch (ReflectionTypeLoadException rtle)
            {
                Log.ErrorFormat("Could not load available types.", rtle);
                foreach (var loaderEx in rtle.LoaderExceptions)
                {
                    Log.ErrorFormat("Type load exception: {0}", loaderEx, loaderEx.Message);
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Could not find available types.", ex);
            }

            return new TAvailableType[0];
        }

        #endregion
    }
}