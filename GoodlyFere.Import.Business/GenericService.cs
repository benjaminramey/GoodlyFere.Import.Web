#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Business
{
    public class GenericService<T> : IGenericService<T>
        where T : IModelBase
    {
        #region Constructors and Destructors

        public GenericService(IWriteRepository<T> repository)
        {
            Repository = repository;
        }

        #endregion

        #region Properties

        protected IWriteRepository<T> Repository { get; private set; }

        #endregion

        #region Public Methods

        public virtual void Delete(IEnumerable<T> entitiesToDelete)
        {
            Repository.DeleteMany(entitiesToDelete.ToArray());
        }

        public virtual T[] GetAll()
        {
            return Repository.GetAll();
        }

        public virtual T GetById(int id)
        {
            return Repository.Get(id);
        }

        public virtual IEnumerable<T> Save(IEnumerable<T> newEntities)
        {
            return Repository.SaveMany(newEntities.ToArray());
        }

        public virtual IEnumerable<T> Update(IEnumerable<T> entities)
        {
            return Repository.UpdateMany(entities.ToArray());
        }

        #endregion
    }
}