#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#endregion

namespace GoodlyFere.Import.Data.Model
{
    public interface IParameterInstance : IModelBase
    {
        #region Public Properties

        string Name { get; set; }
        int RunConfigurationId { get; set; }
        string Value { get; set; }

        [Required]
        string Type { get; set; }

        #endregion
    }
}