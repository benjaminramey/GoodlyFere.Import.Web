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
        public ActionResult Upload(HttpPostedFileBase dll)
        {
            string serverPath = string.Empty;
            if (dll == null && Request.Files.Count == 1)
            {
                dll = Request.Files["dll"];
            }

            if (dll != null && dll.ContentLength > 0)
            {
                string fileName = Path.GetFileName(dll.FileName);
                serverPath = Path.Combine(Server.MapPath(string.Concat("~/", Constants.PluginsRelativePath)), fileName);
                dll.SaveAs(serverPath);
            }

            return Json(serverPath);
        }

        #endregion
    }
}