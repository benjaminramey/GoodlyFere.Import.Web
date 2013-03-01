#region Usings

using System;
using System.Linq;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Data.EntityFramework.Model
{
    public abstract class ModelBase : IModelBase
    {
        #region Public Properties

        public Int32 Id { get; set; }

        #endregion
    }
}