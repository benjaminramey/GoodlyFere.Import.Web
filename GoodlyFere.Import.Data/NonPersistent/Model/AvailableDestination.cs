#region Usings

using System;
using System.Linq;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Data.NonPersistent.Model
{
    public class AvailableDestination : IAvailableDestination
    {
        #region Public Properties

        public string FullTypeName { get; set; }

        #endregion
    }
}