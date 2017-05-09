namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedStatusToPackage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "Status");
        }
    }
}
