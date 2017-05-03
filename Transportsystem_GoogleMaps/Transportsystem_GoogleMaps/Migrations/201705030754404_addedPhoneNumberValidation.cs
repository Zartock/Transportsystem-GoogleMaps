namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedPhoneNumberValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Drivers", "PhoneNumber", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Drivers", "PhoneNumber", c => c.String(nullable: false, maxLength: 15));
        }
    }
}
