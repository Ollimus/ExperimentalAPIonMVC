﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestAPI.Models;

namespace TestApi.SystemTests
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
        }
    }
}
