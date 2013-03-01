namespace GoodlyFere.Import.Data.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPlugin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Plugins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Dll = c.String(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Plugins");
        }
    }
}
