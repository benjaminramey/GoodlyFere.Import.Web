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
    public class AvailableSourcesController : ApiController
    {
        #region Constants and Fields

        private readonly IRepository<IAvailableSource> _repository;

        #endregion

        #region Constructors and Destructors

        public AvailableSourcesController(IRepository<IAvailableSource> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Public Methods

        public IEnumerable<IAvailableSource> Get()
        {
            return _repository.GetAll();
        }

        #endregion
    }
}