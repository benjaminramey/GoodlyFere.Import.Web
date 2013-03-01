#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Business.Interfaces
{
    public interface IRunConfigurationService : IGenericService<IRunConfiguration>
    {
        #region Public Methods

        IEnumerable<IRunConfiguration> GetWithNames();

        IEnumerable<IRunConfiguration> GetWithNamesForProject(int projectId);

        bool Run(IRunConfiguration config);

        #endregion
    }
}