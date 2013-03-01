#region Usings

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Data.EntityFramework.Model
{
    [KnownType(typeof(Project))]
    public class Project : ModelBase, IProject
    {
        #region Public Properties

        public ConverterDefinition ConverterDefinition { get; set; }
        public int ConverterDefinitionId { get; set; }
        public DestinationDefinition DestinationDefinition { get; set; }

        public int DestinationDefinitionId { get; set; }

        [Required]
        public string Name { get; set; }

        public SourceDefinition SourceDefinition { get; set; }

        public int SourceDefinitionId { get; set; }

        #endregion

        #region Explicit Interface Properties

        [NotMapped]
        IConverterDefinition IProject.ConverterDefinition
        {
            get
            {
                return ConverterDefinition;
            }
            set
            {
                ConverterDefinition = (ConverterDefinition)value;
            }
        }

        [NotMapped]
        IDestinationDefinition IProject.DestinationDefinition
        {
            get
            {
                return DestinationDefinition;
            }
            set
            {
                DestinationDefinition = (DestinationDefinition)value;
            }
        }

        [NotMapped]
        ISourceDefinition IProject.SourceDefinition
        {
            get
            {
                return SourceDefinition;
            }
            set
            {
                SourceDefinition = (SourceDefinition)value;
            }
        }

        #endregion
    }
}