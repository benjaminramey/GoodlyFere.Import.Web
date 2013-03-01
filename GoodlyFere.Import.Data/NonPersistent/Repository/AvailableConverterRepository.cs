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
    public class AvailableConverterRepository : TypeCacheRepository<AvailableConverter, IConverter>,
                                                IRepository<IAvailableConverter>
    {
        #region Public Methods

        public IAvailableConverter[] Get(Expression<Func<IAvailableConverter, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IAvailableConverter Get(object key)
        {
            throw new NotImplementedException();
        }

        public IAvailableConverter[] GetAll()
        {
            return Cache.Cast<IAvailableConverter>().ToArray();
        }

        #endregion
    }
}