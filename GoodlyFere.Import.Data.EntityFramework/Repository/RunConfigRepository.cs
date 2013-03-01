#region Usings

using System;
using System.Data.Entity;
using System.Linq;
using GoodlyFere.Import.Data.EntityFramework.Model;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Data.EntityFramework.Repository
{
    public class RunConfigRepository : GenericRepository<IRunConfiguration, RunConfiguration>
    {
        #region Methods

        protected override IQueryable<RunConfiguration> GetQueryable(ImportEntities context)
        {
            return context.RunConfigurations
                          .Include(p => p.ParameterInstances);
        }

        #endregion
    }
}