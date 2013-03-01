#region Usings

using System;
using System.Linq;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Web.Controllers.API
{
    public class ParameterInstancesController : GenericController<IParameterInstance>
    {
        #region Constructors and Destructors

        public ParameterInstancesController(IGenericService<IParameterInstance> genericService)
            : base(genericService)
        {
        }

        #endregion
    }
}