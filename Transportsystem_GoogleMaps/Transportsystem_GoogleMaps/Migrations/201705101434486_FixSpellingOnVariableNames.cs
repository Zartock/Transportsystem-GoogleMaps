namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixSpellingOnVariableNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "PlannedDeliveryDate", c => c.DateTime());
            DropColumn("dbo.Packages", "PlanedDeliveryDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Packages", "PlanedDeliveryDate", c => c.DateTime());
            DropColumn("dbo.Packages", "PlannedDeliveryDate");
        }
    }
}
