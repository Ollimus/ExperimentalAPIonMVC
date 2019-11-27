using System;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using TestAPI.Controllers;

namespace TestAPITests.IntegrationTests.APIController
{
    [TestClass]
    public class CustomersControllerTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsNotNull(baseUrl);
        }
    }
}
