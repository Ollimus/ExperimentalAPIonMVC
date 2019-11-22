using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI.Controllers;

namespace TestAPITests.Unit_Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_UsingHomeController_ReturnsViewResult()
        {
            HomeController _controller = new HomeController();

            var result = _controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About_UsingHomeController_ReturnsViewResult()
        {
            HomeController _controller = new HomeController();

            var result = _controller.ApiGuide() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
