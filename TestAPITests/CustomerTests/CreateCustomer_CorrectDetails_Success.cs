using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace TestAPITests
{
    [TestClass]
    public class CreateCustomer_CorrectDetails_Success
    {
        [TestMethod]
        public void CreateCustomer()
        {
            Customer customer = new Customer()
            {
                CustomerId = 0, //mock
                FirstName = "Kia",
                LastName = "Hankola",
                City = "Helsinki",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            var validationContext = new ValidationContext(customer, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(customer, validationContext, errors, true);

            Assert.IsTrue(isValid);
        }
    }
}
