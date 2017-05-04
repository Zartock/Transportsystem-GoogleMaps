namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPersonalNumberToDriverAndUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Drivers", "PersonalNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Drivers", "PersonalNumber");
        }
    }
}
