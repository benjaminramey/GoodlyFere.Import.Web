#region Usings

using System;
using System.Data.Entity;
using System.Linq;
using GoodlyFere.Import.Data.EntityFramework.Model;

#endregion

namespace GoodlyFere.Import.Data.EntityFramework
{
    public class ImportEntities : DbContext
    {
        #region Constructors and Destructors

        public ImportEntities()
            : base("ImportEntities")
        {
        }

        #endregion

        #region Public Properties

        public DbSet<ConverterDefinition> ConverterDefinitions { get; set; }
        public DbSet<DestinationDefinition> DestinationDefinitions { get; set; }
        public DbSet<ParameterInstance> ParameterInstances { get; set; }
        public DbSet<Plugin> Plugins { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<RunConfiguration> RunConfigurations { get; set; }
        public DbSet<SourceDefinition> SourceDefinitions { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}