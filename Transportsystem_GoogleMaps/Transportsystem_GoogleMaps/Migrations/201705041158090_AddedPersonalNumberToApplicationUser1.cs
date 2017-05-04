namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPersonalNumberToApplicationUser1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Drivers", "PersonalNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Drivers", "PersonalNumber", c => c.String());
        }
    }
}
