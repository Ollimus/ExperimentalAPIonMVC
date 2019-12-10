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
            System.Diagnostics.Debug.Print("Test");

            var configuration = new TestAPI.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
            RemoveAllDataEntries();
        }

        private static void RemoveAllDataEntries()
        {
            ApplicationDbContext _context = new ApplicationDbContext();

            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE Customers;");
            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE Products;");
            _context.Database.ExecuteSqlCommand("DBCC CHECKIDENT(Customers, RESEED, 1);");

            _context.SaveChanges();
            _context.Dispose();
        }
    }
}
