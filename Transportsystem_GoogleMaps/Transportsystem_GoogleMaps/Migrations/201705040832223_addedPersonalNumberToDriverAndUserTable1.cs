namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPersonalNumberToDriverAndUserTable1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PersonalNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PersonalNumber");
        }
    }
}
