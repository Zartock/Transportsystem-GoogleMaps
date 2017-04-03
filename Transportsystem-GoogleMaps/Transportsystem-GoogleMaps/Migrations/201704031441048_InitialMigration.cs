namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Package_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .Index(t => t.Package_Id);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 50),
                        Destination = c.String(nullable: false, maxLength: 255),
                        Priority = c.Byte(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        PhoneNumber = c.String(nullable: false, maxLength: 15),
                        DeliveryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Deliveries", t => t.DeliveryId)
                .Index(t => t.DeliveryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Drivers", "DeliveryId", "dbo.Deliveries");
            DropForeignKey("dbo.Deliveries", "Package_Id", "dbo.Packages");
            DropIndex("dbo.Drivers", new[] { "DeliveryId" });
            DropIndex("dbo.Deliveries", new[] { "Package_Id" });
            DropTable("dbo.Drivers");
            DropTable("dbo.Packages");
            DropTable("dbo.Deliveries");
        }
    }
}
