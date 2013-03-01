#region Usings

using System;
using System.Linq;

#endregion

namespace GoodlyFere.Import.Data.Model
{
    public interface IImporterTypeModel
    {
        #region Public Properties

        string FullTypeName { get; set; }

        #endregion
    }
}