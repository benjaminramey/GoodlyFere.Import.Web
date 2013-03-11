#region Usings

using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GoodlyFere.Import.Data;

#endregion

namespace GoodlyFere.Import.Web.Controllers
{
    public class PluginsController : Controller
    {
        #region Public Methods

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase pluginPackage)
        {
            string serverPath = string.Empty;
            if (pluginPackage == null && Request.Files.Count == 1)
            {
                pluginPackage = Request.Files["pluginPackage"];
            }

            if (pluginPackage != null && pluginPackage.ContentLength > 0)
            {
                string fileName = Path.GetFileName(pluginPackage.FileName);
                string pluginsFolder = Server.MapPath(string.Concat("~/", Constants.PluginsRelativePath));

                if (!Directory.Exists(pluginsFolder))
                {
                    Directory.CreateDirectory(pluginsFolder);
                }

                serverPath = Path.Combine(pluginsFolder, fileName);
                pluginPackage.SaveAs(serverPath);
            }

            return Json(serverPath);
        }

        #endregion
    }
}