#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Data.EntityFramework.Model
{
    public class Plugin : ModelBase, IPlugin
    {
        #region Public Properties

        [Required]
        public string PackagePath { get; set; }

        [Required]
        public string Name { get; set; }

        #endregion
    }
}