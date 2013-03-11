namespace GoodlyFere.Import.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePluginAssemblyPathToPackagePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plugins", "PackagePath", c => c.String(nullable: false));
            DropColumn("dbo.Plugins", "AssemblyPath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plugins", "AssemblyPath", c => c.String(nullable: false));
            DropColumn("dbo.Plugins", "PackagePath");
        }
    }
}
