namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDeliveryRoutes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryRoutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Driver_Id = c.Int(),
                        Package_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivers", t => t.Driver_Id)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .Index(t => t.Driver_Id)
                .Index(t => t.Package_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DeliveryRoutes", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.DeliveryRoutes", "Driver_Id", "dbo.Drivers");
            DropIndex("dbo.DeliveryRoutes", new[] { "Package_Id" });
            DropIndex("dbo.DeliveryRoutes", new[] { "Driver_Id" });
            DropTable("dbo.DeliveryRoutes");
        }
    }
}
