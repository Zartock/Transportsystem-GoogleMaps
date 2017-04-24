namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedRoutePropertyToPackage1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "BelongsToRoute", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "BelongsToRoute");
        }
    }
}
