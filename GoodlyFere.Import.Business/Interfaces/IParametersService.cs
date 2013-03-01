#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Import.Business.Interfaces
{
    public interface IParametersService<T>
    {
        #region Public Methods

        T[] GetForType(string type);

        #endregion
    }
}