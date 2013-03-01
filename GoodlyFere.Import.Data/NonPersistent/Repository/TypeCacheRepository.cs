#region Usings

using System;
using System.Linq;
using System.Reflection;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Data.NonPersistent.Repository
{
    public abstract class TypeCacheRepository<TAvailableType, TInterface>
        where TAvailableType : IImporterTypeModel, new()
    {
        #region Constants and Fields

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
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            return assemblies.SelectMany(a => a.DefinedTypes)
                             .Where(
                                 t => typeof(TInterface).IsAssignableFrom(t)
                                      && !t.IsInterface
                                      && !t.IsAbstract)
                             .Select(t => new TAvailableType { FullTypeName = t.AssemblyQualifiedName })
                             .ToArray();
        }

        #endregion
    }
}