#region Usings

using System;
using System.Data.Entity;
using System.Linq;
using GoodlyFere.Import.Data.EntityFramework.Model;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Data.EntityFramework.Repository
{
    public class ProjectRepository : GenericRepository<IProject, Project>
    {
        #region Methods

        protected override IQueryable<Project> GetQueryable(ImportEntities context)
        {
            return context.Projects
                          .Include(p => p.DestinationDefinition)
                          .Include(p => p.SourceDefinition)
                          .Include(p => p.ConverterDefinition);
        }

        #endregion
    }
}