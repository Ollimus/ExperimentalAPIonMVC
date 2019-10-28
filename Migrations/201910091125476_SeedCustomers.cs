namespace TestAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedCustomers : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Customers] ON
INSERT INTO [dbo].[Customers] ([CustomerId], [FirstName], [LastName]) VALUES (1, N'Teuvo', N'Hakkarainen')
INSERT INTO [dbo].[Customers] ([CustomerId], [FirstName], [LastName]) VALUES (2, N'Jarno', N'Rinne')
INSERT INTO [dbo].[Customers] ([CustomerId], [FirstName], [LastName]) VALUES (3, N'Nella', N'Kiviranta')
INSERT INTO [dbo].[Customers] ([CustomerId], [FirstName], [LastName]) VALUES (4, N'Tiina', N'Tuisku')
SET IDENTITY_INSERT [dbo].[Customers] OFF");
        }
        
        public override void Down()
        {
        }
    }
}
