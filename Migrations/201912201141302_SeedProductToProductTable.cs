namespace TestAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedProductToProductTable : DbMigration
    {
        public override void Up()
        {
            Sql(@"SET IDENTITY_INSERT [dbo].[Products] ON
INSERT INTO [dbo].[Products] ([ProductId], [Name], [Description], [Price], [Stock], [Producer]) VALUES (1, N'Iphone 6', N'Modern phone for modern people.', CAST(13.45 AS Decimal(18, 2)), 7, N'Apple')
SET IDENTITY_INSERT [dbo].[Products] OFF
");
        }
        
        public override void Down()
        {
        }
    }
}
