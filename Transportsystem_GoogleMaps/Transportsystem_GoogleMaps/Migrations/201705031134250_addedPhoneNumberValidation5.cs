namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPhoneNumberValidation5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Packages", "Destination", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Packages", "Destination", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
