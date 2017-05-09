namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingDeliveryDateToDeliveryRoute : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryRoutes", "DeliveryDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DeliveryRoutes", "DeliveryDate");
        }
    }
}
