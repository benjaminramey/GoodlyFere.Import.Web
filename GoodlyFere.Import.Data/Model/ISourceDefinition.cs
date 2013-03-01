#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Import.Data.Model
{
    public interface ISourceDefinition : IModelBase
    {
        #region Public Properties

        string Name { get; set; }
        string Type { get; set; }

        #endregion
    }
}