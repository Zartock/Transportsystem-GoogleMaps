namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingPackageCoOrdsFromStructToDoubles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Latitude", c => c.Double());
            AddColumn("dbo.Packages", "Longitude", c => c.Double());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "Longitude");
            DropColumn("dbo.Packages", "Latitude");
        }
    }
}
