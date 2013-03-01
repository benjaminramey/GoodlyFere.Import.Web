#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using GoodlyFere.Import.Data;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Web.Controllers.API
{
    public class AvailableDestinationsController : ApiController
    {
        #region Constants and Fields

        private readonly IRepository<IAvailableDestination> _repository;

        #endregion

        #region Constructors and Destructors

        public AvailableDestinationsController(IRepository<IAvailableDestination> repository)
        {
            _repository = repository;
        }

        #endregion
        
        #region Public Methods

        public IEnumerable<IAvailableDestination> Get()
        {
            return _repository.GetAll();
        }

        #endregion
    }
}