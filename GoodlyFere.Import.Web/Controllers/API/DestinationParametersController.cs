#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Web.Controllers.API
{
    public class DestinationParametersController : ApiController
    {
        #region Constants and Fields

        private readonly IParametersService<IDestinationParameter> _service;

        #endregion

        #region Constructors and Destructors

        public DestinationParametersController(IParametersService<IDestinationParameter> service)
        {
            _service = service;
        }

        #endregion

        #region Public Methods

        public IEnumerable<IDestinationParameter> Get(string type)
        {
            return _service.GetForType(type);
        }

        #endregion
    }
}