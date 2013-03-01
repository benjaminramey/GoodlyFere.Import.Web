#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Import.Data
{
    public interface IWriteRepository<T> : IRepository<T>
    {
        #region Public Methods

        void Delete(T objectToDelete);

        void DeleteMany(T[] objectsToDelete);

        T Save(T newObject);

        T[] SaveMany(T[] newObjects);

        T Update(T updatedObject);

        T[] UpdateMany(T[] updatedObjects);

        #endregion
    }
}