#region Usings

using System;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace GoodlyFere.Import.Data
{
    public interface IRepository<T>
    {
        #region Public Methods

        T[] Get(Expression<Func<T, bool>> filter);

        T Get(object key);

        T[] GetAll();

        #endregion
    }
}