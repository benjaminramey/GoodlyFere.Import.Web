#region Usings

using System;
using System.Linq;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data;

#endregion

namespace GoodlyFere.Import.Business
{
    public class ParametersService<T> : IParametersService<T>
    {
        #region Constants and Fields

        private readonly IRepository<T[]> _repository;

        #endregion

        #region Constructors and Destructors

        public ParametersService(IRepository<T[]> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public T[] GetForType(string type)
        {
            return _repository.Get(type);
        }

        #endregion
    }
}