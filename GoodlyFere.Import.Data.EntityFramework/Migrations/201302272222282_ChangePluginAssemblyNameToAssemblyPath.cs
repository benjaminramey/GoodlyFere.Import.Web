namespace GoodlyFere.Import.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePluginAssemblyNameToAssemblyPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plugins", "AssemblyPath", c => c.String(nullable: false));
            DropColumn("dbo.Plugins", "AssemblyName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plugins", "AssemblyName", c => c.String(nullable: false));
            DropColumn("dbo.Plugins", "AssemblyPath");
        }
    }
}
