#region Usings

using System;
using System.Data.Entity.Migrations;
using System.Linq;

#endregion

namespace GoodlyFere.Import.Data.EntityFramework.Migrations
{
    #region Usings

    using System;
    using System.Linq;

    #endregion

    internal sealed class Configuration :
        DbMigrationsConfiguration<GoodlyFere.Import.Data.EntityFramework.ImportEntities>
    {
        #region Constructors and Destructors

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        #endregion

        #region Methods

        protected override void Seed(GoodlyFere.Import.Data.EntityFramework.ImportEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }

        #endregion
    }
}