﻿using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI.Controllers;

namespace TestAPITests.Unit_Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_UsingHomeController_ReturnsEmptyString()
        {
            HomeController controller = new HomeController();

            var result = controller.Index() as ViewResult;

            /*
            * If the return type of function is View() with no parameter, it defaults to empty string. If the string is empty, it search for view using function name.
            * If View has a parameter to a different view or redirecting, result.ViewName will have an value.
            */
            Assert.AreEqual(string.Empty, result.ViewName);
        }

        [TestMethod]
        public void FindIndexView_CheckExistanceOfFile_ReturnsView()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult; 

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About_UsingHomeController_ReturnsEmptyString()
        {
            HomeController controller = new HomeController();

            var result = controller.ApiGuide() as ViewResult;

            /*
            * If the return type of function is View() with no parameter, it defaults to empty string. If the string is empty, it search for view using function name.
            * If View has a parameter to a different view or redirecting, result.ViewName will have an value.
            */
            Assert.AreEqual(string.Empty, result.ViewName);
        }
    }
}