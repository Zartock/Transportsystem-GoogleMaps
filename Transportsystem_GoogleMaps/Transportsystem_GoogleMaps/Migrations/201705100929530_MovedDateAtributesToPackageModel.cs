namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovedDateAtributesToPackageModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "PlanedDeliveryDate", c => c.DateTime());
            AddColumn("dbo.Packages", "DateDelivered", c => c.DateTime());
            DropColumn("dbo.DeliveryRoutes", "PlanedDeliveryDate");
            DropColumn("dbo.DeliveryRoutes", "DateDelivered");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeliveryRoutes", "DateDelivered", c => c.DateTime());
            AddColumn("dbo.DeliveryRoutes", "PlanedDeliveryDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Packages", "DateDelivered");
            DropColumn("dbo.Packages", "PlanedDeliveryDate");
        }
    }
}
