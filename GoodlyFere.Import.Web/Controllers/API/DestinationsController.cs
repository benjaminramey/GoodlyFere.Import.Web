#region Usings

using System;
using System.Linq;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Web.Controllers.API
{
    public class DestinationsController : GenericController<IDestinationDefinition>
    {
        #region Constructors and Destructors

        public DestinationsController(IGenericService<IDestinationDefinition> genericService)
            : base(genericService)
        {
        }

        #endregion
    }
}