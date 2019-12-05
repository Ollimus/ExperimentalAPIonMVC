using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.IntegrationTests
{
    [TestClass]
    public class GlobalSetup
    {
        [TestInitialize]
        public void SetUp()
        {
            var configuration = new TestAPI.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }
    }
}
