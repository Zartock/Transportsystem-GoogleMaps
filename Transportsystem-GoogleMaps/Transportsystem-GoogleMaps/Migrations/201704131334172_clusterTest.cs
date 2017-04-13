namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clusterTest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deliveries", "numOfDrivers", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Deliveries", "numOfDrivers");
        }
    }
}
