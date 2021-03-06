﻿#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Data.NonPersistent.Model
{
    [KnownType(typeof(DestinationParameter))]
    public class DestinationParameter : IDestinationParameter
    {
        #region Public Properties

        [Required]
        public string Name { get; set; }

        #endregion
    }
}