#region Usings

using System.Collections.Generic;
using System.Linq;
using System;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Business.Interfaces
{
    public interface IGenericService<T>
        where T : IModelBase
    {
        #region Public Methods

        void Delete(IEnumerable<T> entitiesToDelete);

        T[] GetAll();

        T GetById(int id);
        
        IEnumerable<T> Save(IEnumerable<T> newEntities);

        IEnumerable<T> Update(IEnumerable<T> entities);

        #endregion
    }
}