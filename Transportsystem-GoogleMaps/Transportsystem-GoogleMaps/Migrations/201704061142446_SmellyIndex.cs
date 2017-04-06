namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SmellyIndex : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Index", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "Index");
        }
    }
}
