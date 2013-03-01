#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GoodlyFere.Import.Business.Interfaces;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Web.Controllers.API
{
    public class RunConfigurationsController : GenericController<IRunConfiguration>
    {
        #region Constructors and Destructors

        public RunConfigurationsController(IRunConfigurationService configService)
            : base(configService)
        {
        }

        #endregion

        #region Properties

        private IRunConfigurationService ConfigService
        {
            get
            {
                return (IRunConfigurationService)Service;
            }
        }

        #endregion

        #region Public Methods

        public IEnumerable<IRunConfiguration> GetByProjectId(int projectId)
        {
            return ConfigService.GetWithNamesForProject(projectId);
        }

        [AcceptVerbs("RUN")]
        public void Run([FromBody] IRunConfiguration config)
        {
            if (!ConfigService.Run(config))
            {
                Request.CreateErrorResponse(HttpStatusCode.SeeOther, "Run failed");
            }
        }

        #endregion
    }
}