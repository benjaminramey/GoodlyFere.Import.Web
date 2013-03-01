#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Data.EntityFramework.Model
{
    [KnownType(typeof(DestinationDefinition))]
    public class DestinationDefinition : ModelBase, IDestinationDefinition
    {
        #region Public Properties

        [Required]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        #endregion
    }
}