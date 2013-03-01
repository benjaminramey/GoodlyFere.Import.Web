#region Usings

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace GoodlyFere.Import.Data.Model
{
    public interface IRunConfiguration : IModelBase
    {
        #region Public Properties

        string Name { get; set; }
        ICollection<IParameterInstance> ParameterInstances { get; set; }
        IProject Project { get; set; }
        int ProjectId { get; set; }

        #endregion
    }
}