namespace GoodlyFere.Import.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangePluginDllToAssemblyName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plugins", "AssemblyPath", c => c.String(nullable: false));
            DropColumn("dbo.Plugins", "Dll");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plugins", "Dll", c => c.String(nullable: false));
            DropColumn("dbo.Plugins", "AssemblyPath");
        }
    }
}
