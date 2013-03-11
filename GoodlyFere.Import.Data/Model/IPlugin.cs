#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Import.Data.Model
{
    public interface IPlugin : IModelBase
    {
        #region Public Properties

        string PackagePath { get; set; }
        string Name { get; set; }

        #endregion
    }
}