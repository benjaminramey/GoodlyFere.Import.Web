#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Web.Models
{
    public class InitialDataViewModel
    {
        #region Public Properties

        public IEnumerable<IProject> Projects { get; set; }

        public string ProjectsJson { get; set; }

        #endregion
    }
}