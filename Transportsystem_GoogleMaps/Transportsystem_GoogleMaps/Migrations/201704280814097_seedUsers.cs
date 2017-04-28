namespace Transportsystem_GoogleMaps.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedUsers : DbMigration
    {
        public override void Up()
        {

            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'cebf3980-c7e9-4efb-8698-58cd9ecad708', N'admin1@gmail.com', 0, N'APYq5w40PmQ7bYCaAuktSE+tQ4u87YQCLefGQO0gU45jY+FSk6YjfP4+E82l+dD7kw==', N'967f953d-bfc9-4ada-90de-5438c8f3ce49', NULL, 0, 0, NULL, 1, 0, N'admin1@gmail.com')
");

            Sql(@"INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'df3eb72e-1928-4429-865d-797f8f16eeac', N'AdminRole')
");
            Sql(@"INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'cebf3980-c7e9-4efb-8698-58cd9ecad708', N'df3eb72e-1928-4429-865d-797f8f16eeac')
");

        }
        
        public override void Down()
        {
        }
    }
}
