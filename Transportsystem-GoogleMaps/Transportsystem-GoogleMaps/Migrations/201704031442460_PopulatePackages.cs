namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatePackages : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Packages(Content, Destination, Priority) VALUES('Pen', 'Örebro Universitet', 3)");
            Sql("INSERT INTO Packages(Content, Destination, Priority) VALUES('Pineapple', 'Örebro Universitet', 3)");
            Sql("INSERT INTO Packages(Content, Destination, Priority) VALUES('Apple', 'Örebro Universitet', 3)");
        }
        
        public override void Down()
        {
        }
    }
}
