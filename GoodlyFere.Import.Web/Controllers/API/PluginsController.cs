#region Usings

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Web.Controllers.API
{
    public class PluginsController : GenericController<IPlugin>
    {
        #region Constructors and Destructors

        public PluginsController(IPluginService pluginService)
            : base(pluginService)
        {
        }

        #endregion

        #region Properties

        private IPluginService PluginService
        {
            get
            {
                return (IPluginService)Service;
            }
        }

        #endregion

        #region Public Methods

        [AcceptVerbs("RELOAD")]
        public void Reload([FromBody] IPlugin plugin)
        {
            if (!PluginService.Reload(plugin))
            {
                Request.CreateErrorResponse(HttpStatusCode.SeeOther, "Reload failed");
            }
        }

        #endregion
    }
}