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
                Description = "Gaming keyboard for many uses.",
                Price = 120,
                Producer = "Logitech",
                Stock = 10
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void CreateProduct_NoProperties_ValidationFailure()
        {
            Product product = new Product();

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void CreateProduct_NamePropertyTooShort_ReturnError()
        {
            string errorMessage = "Minimum of 2 characters.";
            int errorMessageCount = 0;

            Product product = new Product()
            {
                ProductId = 0,
                Name = "G",
                Description = "Gaming keyboard for many uses.",
                Price = 120,
                Producer = "Logitech",
                Stock = 10
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (errors.Count > 1)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errorMessageCount);
        }

        [TestMethod]
        public void CreateProduct_NamePropertyTooLong_ReturnError()
        {
            string errorMessage = "Only 100 characters allowed.";
            int errorMessageCount = 0;
            string testString = new string('a', 101);

            Product product = new Product()
            {
                ProductId = 0,
                Name = testString,
                Description = "Gaming keyboard for many uses.",
                Price = 120,
                Producer = "Logitech",
                Stock = 10
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (errors.Count > 1)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errorMessageCount);
        }

        [TestMethod]
        public void CreateProduct_DescriptionValueTooShort_ReturnError()
        {
            string errorMessage = "Minimum of 3 characters.";
            int errorMessageCount = 0;

            Product product = new Product()
            {
                ProductId = 0,
                Name = "G15 Keyboard",
                Description = "Ga",
                Price = 120,
                Producer = "Logitech",
                Stock = 10
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (errors.Count > 1)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errorMessageCount);
        }

        [TestMethod]
        public void CreateProduct_DescriptionPropertyTooLong_ReturnError()
        {
            string errorMessage = "Only 255 characters allowed.";
            int errorMessageCount = 0;
            string testString = new string('a', 256);

            Product product = new Product()
            {
                ProductId = 0,
                Name = "G15 Gaming Keyboard",
                Description = testString,
                Price = 120,
                Producer = "Logitech",
                Stock = 10
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (errors.Count > 1)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errorMessageCount);
        }

        [TestMethod]
        public void CreateProduct_ProducerValueTooShort_ReturnError()
        {
            string errorMessage = "Minimum of 3 characters.";
            int errorMessageCount = 0;

            Product product = new Product()
            {
                ProductId = 0,
                Name = "G15 Keyboard",
                Description = "Gaming keyboard for many uses.",
                Price = 120,
                Producer = "Lo",
                Stock = 10
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (errors.Count > 1)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errorMessageCount);
        }

        [TestMethod]
        public void CreateProduct_ProducerPropertyTooLong_ReturnError()
        {
            string errorMessage = "Only 40 characters allowed.";
            int errorMessageCount = 0;
            string testString = new string('a', 41);

            Product product = new Product()
            {
                ProductId = 0,
                Name = "G15 Gaming Keyboard",
                Description = "Gaming keyboard for many uses.",
                Price = 120,
                Producer = testString,
                Stock = 10
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (errors.Count > 1)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errorMessageCount);
        }

        [TestMethod]
        public void CreateProduct_PricePropertyIsZero_ReturnError()
        {
            string errorMessage = "The price must be between 1 and 5000.";
            int errorMessageCount = 0;

            Product product = new Product()
            {
                ProductId = 0,
                Name = "G15 Gaming Keyboard",
                Description = "Gaming keyboard for many uses.",
                Price = 0,
                Producer = "Logitech",
                Stock = 10
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (errors.Count > 1)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errorMessageCount);
        }

        [TestMethod]
        public void CreateProduct_PricePropertyIsOver5000Limit_ReturnError()
        {
            string errorMessage = "The price must be between 1 and 5000.";
            int errorMessageCount = 0;
            int testableNumber = 5001;

            Product product = new Product()
            {
                ProductId = 0,
                Name = "G15 Gaming Keyboard",
                Description = "Gaming keyboard for many uses.",
                Price = testableNumber,
                Producer = "Logitech",
                Stock = 10
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (errors.Count > 1)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errorMessageCount);
        }

        [TestMethod]
        public void CreateProduct_PricePropertyHasTooManyDecimalPlaces_ReturnError()
        {
            string errorMessage = "Maximum of two decimal places allowed.";
            int errorMessageCount = 0;
            decimal testableNumber = 211.211m;

            Product product = new Product()
            {
                ProductId = 0,
                Name = "G15 Gaming Keyboard",
                Description = "Gaming keyboard for many uses.",
                Price = testableNumber,
                Producer = "Logitech",
                Stock = 10
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (errors.Count > 1)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errorMessageCount);
        }

        [TestMethod]
        public void CreateProduct_PricePropertyHasNegativeValue_ReturnError()
        {
            string errorMessage = "The price must be between 1 and 5000.";
            int errorMessageCount = 0;
            decimal testableNumber = -11.21m;

            Product product = new Product()
            {
                ProductId = 0,
                Name = "G15 Gaming Keyboard",
                Description = "Gaming keyboard for many uses.",
                Price = testableNumber,
                Producer = "Logitech",
                Stock = 10
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (errors.Count > 1)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errorMessageCount);
        }

        [TestMethod]
        public void CreateProduct_StockNegativeValue_ReturnError()
        {
            string errorMessage = "Stock must be between 1 and 500.";
            int errorMessageCount = 0;
            int testableNumber = -11;

            Product product = new Product()
            {
                ProductId = 0,
                Name = "G15 Gaming Keyboard",
                Description = "Gaming keyboard for many uses.",
                Price = 120,
                Producer = "Logitech",
                Stock = testableNumber
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (errors.Count > 1)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errorMessageCount);
        }

        [TestMethod]
        public void CreateProduct_StockValueOverLimit_ReturnError()
        {
            string errorMessage = "Stock must be between 1 and 500.";
            int errorMessageCount = 0;
            int testableNumber = 501;

            Product product = new Product()
            {
                ProductId = 0,
                Name = "G15 Gaming Keyboard",
                Description = "Gaming keyboard for many uses.",
                Price = 120,
                Producer = "Logitech",
                Stock = testableNumber
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (errors.Count > 1)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errorMessageCount);
        }

        [TestMethod]
        public void CreateProduct_StockValueIsZero_ReturnError()
        {
            string errorMessage = "Stock must be between 1 and 500.";
            int errorMessageCount = 0;
            int testableNumber = 0;

            Product product = new Product()
            {
                ProductId = 0,
                Name = "G15 Gaming Keyboard",
                Description = "Gaming keyboard for many uses.",
                Price = 120,
                Producer = "Logitech",
                Stock = testableNumber
            };

            var validationContext = new ValidationContext(product, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(product, validationContext, errors, true);

            if (errors.Count > 1)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, errorMessageCount);
        }
    }
}
