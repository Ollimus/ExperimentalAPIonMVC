using System;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI.Controllers;

namespace TestAPITests.UnitTests.Controllers
{
    [TestClass]
    public class CustomerControllerTest
    {
        [TestMethod]
        public void Customers_GetViewFromCustomerController_ReturnCorrectViewResult()
        {
            CustomerController _controller = new CustomerController();

            var result = _controller.Customers() as ViewResult;

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CustomerManagement_GetViewFromCustomerController_ReturnNewCustomerFormViewResult()
        {
            CustomerController _controller = new CustomerController();

            var result = _controller.CustomerManagement(0) as ViewResult;

            Assert.AreEqual("NewCustomerForm", result.ViewName);
        }

        [TestMethod]
        public void CustomerManagement_GetCustomerObject_ReturnCustomerObject()
        {
            CustomerController _controller = new CustomerController();

            var result = _controller.CustomerManagement(0) as ViewResult;

            Assert.IsNotNull(result.Model);
        }
    }
}
