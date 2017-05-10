namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDeliveredDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryRoutes", "PlanedDeliveryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.DeliveryRoutes", "DateDelivered", c => c.DateTime());
            DropColumn("dbo.DeliveryRoutes", "DeliveryDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeliveryRoutes", "DeliveryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.DeliveryRoutes", "DateDelivered");
            DropColumn("dbo.DeliveryRoutes", "PlanedDeliveryDate");
        }
    }
}
