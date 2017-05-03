namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeDeliveryFromDriver : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Packages", "Delivery_Id", "dbo.Deliveries");
            DropForeignKey("dbo.Drivers", "DeliveryId", "dbo.Deliveries");
            DropIndex("dbo.Drivers", new[] { "DeliveryId" });
            DropIndex("dbo.Packages", new[] { "Delivery_Id" });
            DropColumn("dbo.Drivers", "DeliveryId");
            DropColumn("dbo.Packages", "Delivery_Id");
            DropTable("dbo.Deliveries");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalDistance = c.Double(),
                        numOfDrivers = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Packages", "Delivery_Id", c => c.Int());
            AddColumn("dbo.Drivers", "DeliveryId", c => c.Int());
            CreateIndex("dbo.Packages", "Delivery_Id");
            CreateIndex("dbo.Drivers", "DeliveryId");
            AddForeignKey("dbo.Drivers", "DeliveryId", "dbo.Deliveries", "Id");
            AddForeignKey("dbo.Packages", "Delivery_Id", "dbo.Deliveries", "Id");
        }
    }
}
