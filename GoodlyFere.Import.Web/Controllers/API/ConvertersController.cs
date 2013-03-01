#region Usings

using System;
using System.Linq;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Web.Controllers.API
{
    public class ConvertersController : GenericController<IConverterDefinition>
    {
        #region Constructors and Destructors

        public ConvertersController(IGenericService<IConverterDefinition> genericService)
            : base(genericService)
        {
        }

        #endregion
    }
}