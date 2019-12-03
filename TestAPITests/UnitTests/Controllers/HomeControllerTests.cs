using System;
using System.Web;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestAPI.Controllers;
using TestAPI.Repositories;
using TestAPI.Models;

namespace TestAPITests.Unit_Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        private HomeController _controller;

        public HomeControllerTests()
        {
            _controller = new HomeController();
        }

        [TestMethod]
        public void Index_UsingHomeController_ReturnsViewResult()
        {
            var result = _controller.Index() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About_UsingHomeController_ReturnsViewResult()
        {
            var result = _controller.ApiGuide() as ViewResult;

            Assert.IsNotNull(result);
        }
    }
}
