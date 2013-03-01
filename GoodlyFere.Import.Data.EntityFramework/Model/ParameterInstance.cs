#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Data.EntityFramework.Model
{
    public class ParameterInstance :
        ModelBase,
        IParameterInstance
    {
        #region Public Properties

        [Required]
        public string Name { get; set; }

        public int RunConfigurationId { get; set; }

        [Required]
        public string Type { get; set; }

        public string Value { get; set; }

        #endregion
    }
}