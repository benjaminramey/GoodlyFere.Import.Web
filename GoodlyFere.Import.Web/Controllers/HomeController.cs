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

        private readonly IPluginService _pluginService;
        private readonly IGenericService<IProject> _projectService;

        #endregion

        #region Constructors and Destructors

        public HomeController(IGenericService<IProject> projectService, IPluginService pluginService)
        {
            _projectService = projectService;
            _pluginService = pluginService;
        }

        #endregion

        #region Public Methods

        public ActionResult Index()
        {
            _pluginService.LoadAll();

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