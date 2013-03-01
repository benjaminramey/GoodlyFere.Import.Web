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
    public class AvailableDestinationRepository : TypeCacheRepository<AvailableDestination, IDestination>,
                                                  IRepository<IAvailableDestination>
    {
        #region Public Methods

        public IAvailableDestination[] Get(Expression<Func<IAvailableDestination, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public IAvailableDestination Get(object key)
        {
            throw new NotImplementedException();
        }

        public IAvailableDestination[] GetAll()
        {
            return Cache.Cast<IAvailableDestination>().ToArray();
        }

        #endregion
    }
}