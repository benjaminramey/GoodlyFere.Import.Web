#region Usings

using System;
using System.Linq;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Web.Controllers.API
{
    public class ProjectsController : GenericController<IProject>
    {
        #region Constructors and Destructors

        public ProjectsController(IGenericService<IProject> projectService)
            : base(projectService)
        {
        }

        #endregion
    }
}