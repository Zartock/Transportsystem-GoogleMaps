namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRoutePropertyToPackage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "AssignedToRoute", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "AssignedToRoute");
        }
    }
}
