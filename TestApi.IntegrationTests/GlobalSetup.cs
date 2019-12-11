using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAPI.Models;

namespace TestApi.IntegrationTests
{
    [TestClass]
    public class GlobalSetup
    {
        [AssemblyInitialize()]
        public static void SetUp(TestContext context)
        {
            var configuration = new TestAPI.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();

            SetUpDatabase();
        }

        private static void SetUpDatabase()
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE Customers;");
            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE Products;");
            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE Orders;");


//            _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[Customers] ON
//INSERT INTO [dbo].[Customers] ([CustomerId], [FirstName], [LastName], [City], [Address], [DateCreated]) VALUES (1, N'Teuvo', N'Hakkarainen', N'', N'', N'1900-01-01 00:00:00')
//SET IDENTITY_INSERT [dbo].[Customers] OFF");
//            _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[Products] ON
//INSERT INTO [dbo].[Products] ([ProductId], [Name], [Description], [Price], [Stock], [Producer]) VALUES (1, N'G15 Keyboard', N'Gaming Keyboard for Gaming uses.', CAST(110.15 AS Decimal(18, 2)), 3, N'Logitech')
//SET IDENTITY_INSERT [dbo].[Products] OFF");
//            _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT [dbo].[Orders] ON
//INSERT INTO [dbo].[Orders] ([Id], [CustomerId], [ProductId], [OrderAmount], [TotalPrice], [TimeAdded]) VALUES (2, 1, 1, 3, CAST(330.45 AS Decimal(18, 2)), N'2019-12-16 00:00:00')
//SET IDENTITY_INSERT [dbo].[Orders] OFF");

            _context.SaveChanges();
            _context.Dispose();
        }
    }
}
