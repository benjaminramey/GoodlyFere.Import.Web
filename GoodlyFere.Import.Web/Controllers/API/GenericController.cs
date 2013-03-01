#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Autofac;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data.Model;
using GoodlyFere.Import.Web.Lib;
using GoodlyFere.Import.Web.Lib.Filters;

#endregion

namespace GoodlyFere.Import.Web.Controllers.API
{
    public class GenericController<T> : ApiController
        where T : IModelBase
    {
        #region Constructors and Destructors

        public GenericController(IGenericService<T> genericService)
        {
            Service = genericService;
        }

        #endregion

        #region Properties

        protected IGenericService<T> Service { get; set; }

        #endregion

        #region Public Methods

        public virtual void Delete(int id)
        {
            var entity = Get(id);
            Service.Delete(new[] { entity });
        }

        // GET api/entities
        public virtual IEnumerable<T> Get()
        {
            return Service.GetAll();
        }

        // GET api/entities/5
        public virtual T Get(int id)
        {
            return Service.GetById(id);
        }

        // POST api/entities
        [ValidationActionFilter]
        public virtual T Post([FromBody] T entity)
        {
            entity = Service.Save(new[] { entity }).FirstOrDefault();
            return entity;
        }

        // PUT api/entities/5
        [ValidationActionFilter]
        public virtual T Put(int id, [FromBody] T entity)
        {
            entity = Service.Update(new[] { entity }).FirstOrDefault();
            return entity;
        }

        #endregion
    }
}