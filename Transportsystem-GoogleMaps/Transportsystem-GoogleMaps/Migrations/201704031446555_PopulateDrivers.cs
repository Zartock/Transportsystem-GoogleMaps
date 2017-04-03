namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateDrivers : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Drivers(Name, PhoneNumber) VALUES('Kjell Mårdensjö', '0701112229')");
        }
        
        public override void Down()
        {
        }
    }
}
