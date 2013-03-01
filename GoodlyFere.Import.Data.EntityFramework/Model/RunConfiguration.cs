#region Usings

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using GoodlyFere.Import.Data.Model;

#endregion

namespace GoodlyFere.Import.Data.EntityFramework.Model
{
    [KnownType(typeof(RunConfiguration))]
    public class RunConfiguration : ModelBase, IRunConfiguration
    {
        #region Public Properties

        [Required]
        public string Name { get; set; }

        public ICollection<ParameterInstance> ParameterInstances { get; set; }

        public Project Project { get; set; }

        public int ProjectId { get; set; }

        #endregion

        #region Explicit Interface Properties

        [NotMapped]
        ICollection<IParameterInstance> IRunConfiguration.ParameterInstances
        {
            get
            {
                return
                    new Collection<IParameterInstance>(ParameterInstances.Cast<IParameterInstance>().ToList());
            }
            set
            {
                ParameterInstances = new Collection<ParameterInstance>(
                    value.Cast<ParameterInstance>().ToList());
            }
        }

        [NotMapped]
        IProject IRunConfiguration.Project
        {
            get
            {
                return Project;
            }
            set
            {
                Project = (Project)value;
            }
        }

        #endregion
    }
}