#region Usings

using System;
using System.Linq;
using System.Web.Mvc;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data.Model;
using GoodlyFere.Import.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

#endregion

namespace GoodlyFere.Import.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Constants and Fields

        private readonly IGenericService<IProject> _projectService;

        #endregion

        #region Constructors and Destructors

        public HomeController(IGenericService<IProject> projectService)
        {
            _projectService = projectService;
        }

        #endregion

        #region Public Methods

        public ActionResult Index()
        {
            var model = new InitialDataViewModel { Projects = _projectService.GetAll() };
            model.ProjectsJson = JsonConvert.SerializeObject(
                model.Projects,
                Formatting.None,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            return View(model);
        }

        #endregion
    }
}