namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedRoutePropertyFromPackage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Packages", "AssignedToRoute");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "AssignedToRoute", c => c.Int());
        }
    }
}
