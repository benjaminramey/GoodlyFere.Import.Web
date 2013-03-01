#region Usings

using System;
using System.Linq;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Web.Controllers.API
{
    public class SourcesController : GenericController<ISourceDefinition>
    {
        #region Constructors and Destructors

        public SourcesController(IGenericService<ISourceDefinition> genericService)
            : base(genericService)
        {
        }

        #endregion
    }
}