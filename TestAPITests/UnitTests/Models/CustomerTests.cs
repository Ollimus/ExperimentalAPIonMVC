using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TestAPITests
{
    [TestClass]
    public class CustomerTests
    {
        [TestMethod]
        public void CreateCustomer_CorrectDetails_ValidationSuccess()
        {
            Customer customer = new Customer()
            {
                CustomerId = 0, //Not required, but put in as mock.
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

        [TestMethod]
        public void CreateCustomer_NoProperties_ValidationFailure()
        {
            Customer customer = new Customer();

            var validationContext = new ValidationContext(customer, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(customer, validationContext, errors, true);

            Assert.IsFalse(isValid);
        }
    }
}
