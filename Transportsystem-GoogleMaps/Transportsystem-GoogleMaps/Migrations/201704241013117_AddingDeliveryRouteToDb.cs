namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDeliveryRouteToDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryRoutes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Driver_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Drivers", t => t.Driver_Id)
                .Index(t => t.Driver_Id);
            
            AddColumn("dbo.Packages", "DeliveryRoute_Id", c => c.Int());
            CreateIndex("dbo.Packages", "DeliveryRoute_Id");
            AddForeignKey("dbo.Packages", "DeliveryRoute_Id", "dbo.DeliveryRoutes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Packages", "DeliveryRoute_Id", "dbo.DeliveryRoutes");
            DropForeignKey("dbo.DeliveryRoutes", "Driver_Id", "dbo.Drivers");
            DropIndex("dbo.Packages", new[] { "DeliveryRoute_Id" });
            DropIndex("dbo.DeliveryRoutes", new[] { "Driver_Id" });
            DropColumn("dbo.Packages", "DeliveryRoute_Id");
            DropTable("dbo.DeliveryRoutes");
        }
    }
}
