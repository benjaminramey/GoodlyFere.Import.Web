#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;
using GoodlyFere.Import.Data.Model;
using GoodlyFere.Import.Data.NonPersistent.Model;
using GoodlyFere.Import.Interfaces;

#endregion

namespace GoodlyFere.Import.Data.NonPersistent.Repository
{
    public class AvailableSourceRepository : TypeCacheRepository<AvailableSource, ISource>,
                                             IRepository<IAvailableSource>
    {
        #region Public Methods

        public IAvailableSource[] Get(Expression<Func<IAvailableSource, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IAvailableSource Get(object key)
        {
            throw new NotImplementedException();
        }

        public IAvailableSource[] GetAll()
        {
            return Cache.Cast<IAvailableSource>().ToArray();
        }

        #endregion
    }
}