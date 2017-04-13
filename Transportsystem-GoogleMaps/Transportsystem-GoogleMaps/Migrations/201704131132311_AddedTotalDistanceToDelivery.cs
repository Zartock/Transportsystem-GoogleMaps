namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTotalDistanceToDelivery : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Deliveries", "Package_Id", "dbo.Packages");
            DropIndex("dbo.Deliveries", new[] { "Package_Id" });
            AddColumn("dbo.Deliveries", "TotalDistance", c => c.Double());
            AddColumn("dbo.Packages", "Delivery_Id", c => c.Int());
            AlterColumn("dbo.Packages", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Packages", "Longitude", c => c.Double(nullable: false));
            CreateIndex("dbo.Packages", "Delivery_Id");
            AddForeignKey("dbo.Packages", "Delivery_Id", "dbo.Deliveries", "Id");
            DropColumn("dbo.Deliveries", "Package_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Deliveries", "Package_Id", c => c.Int());
            DropForeignKey("dbo.Packages", "Delivery_Id", "dbo.Deliveries");
            DropIndex("dbo.Packages", new[] { "Delivery_Id" });
            AlterColumn("dbo.Packages", "Longitude", c => c.Double());
            AlterColumn("dbo.Packages", "Latitude", c => c.Double());
            DropColumn("dbo.Packages", "Delivery_Id");
            DropColumn("dbo.Deliveries", "TotalDistance");
            CreateIndex("dbo.Deliveries", "Package_Id");
            AddForeignKey("dbo.Deliveries", "Package_Id", "dbo.Packages", "Id");
        }
    }
}
