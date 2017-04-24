namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedDeliveryRoute : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Packages", "DeliveryRoute_Id", "dbo.DeliveryRoutes");
            DropForeignKey("dbo.DeliveryRoutes", "Driver_Id", "dbo.Drivers");
            DropIndex("dbo.DeliveryRoutes", new[] { "Driver_Id" });
            DropIndex("dbo.Packages", new[] { "DeliveryRoute_Id" });
            AddColumn("dbo.DeliveryRoutes", "Package_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.DeliveryRoutes", "Driver_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.DeliveryRoutes", "Driver_Id");
            CreateIndex("dbo.DeliveryRoutes", "Package_Id");
            AddForeignKey("dbo.DeliveryRoutes", "Package_Id", "dbo.Packages", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DeliveryRoutes", "Driver_Id", "dbo.Drivers", "Id", cascadeDelete: true);
            DropColumn("dbo.Packages", "DeliveryRoute_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "DeliveryRoute_Id", c => c.Int());
            DropForeignKey("dbo.DeliveryRoutes", "Driver_Id", "dbo.Drivers");
            DropForeignKey("dbo.DeliveryRoutes", "Package_Id", "dbo.Packages");
            DropIndex("dbo.DeliveryRoutes", new[] { "Package_Id" });
            DropIndex("dbo.DeliveryRoutes", new[] { "Driver_Id" });
            AlterColumn("dbo.DeliveryRoutes", "Driver_Id", c => c.Int());
            DropColumn("dbo.DeliveryRoutes", "Package_Id");
            CreateIndex("dbo.Packages", "DeliveryRoute_Id");
            CreateIndex("dbo.DeliveryRoutes", "Driver_Id");
            AddForeignKey("dbo.DeliveryRoutes", "Driver_Id", "dbo.Drivers", "Id");
            AddForeignKey("dbo.Packages", "DeliveryRoute_Id", "dbo.DeliveryRoutes", "Id");
        }
    }
}
