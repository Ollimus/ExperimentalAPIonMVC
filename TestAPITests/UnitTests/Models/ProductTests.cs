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
    public class ProductTests
    {
        [TestMethod]
        public void CreateProduct_CorrectDetails_ValidationSuccess()
        {
            Product product = new Product()
            {
                ProductId = 0,
                Name = "G15 Keyboard",
                Description = "Logitech"
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void CreateCustomer_NoProperties_ValidationFailure()
        {
            Product product = new Product();

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            Assert.IsFalse(isValid);
        }
    }
}
