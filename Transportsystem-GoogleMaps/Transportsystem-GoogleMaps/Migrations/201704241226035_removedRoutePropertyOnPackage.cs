namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedRoutePropertyOnPackage : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Packages", "BelongsToRoute");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "BelongsToRoute", c => c.Int());
        }
    }
}
