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

        [TestMethod]
        public void CreateCustomer_FirstNameValueTooShort_ReturnError()
        {
            string errorMessage = "Minimum of 2 characters.";
            int errorMessageCount = 0;

            Customer customer = new Customer()
            {
                CustomerId = 0, //Not required, but put in as mock.
                FirstName = "K",
                LastName = "Hankila",
                City = "Helsinki",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            var validationContext = new ValidationContext(customer, null, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(customer, validationContext, errors, true);

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
        public void CreateCustomer_LastNameValueTooShort_ReturnError()
        {
            string errorMessage = "Minimum of 2 characters.";
            int errorMessageCount = 0;

            Customer customer = new Customer()
            {
                CustomerId = 0, //Not required, but put in as mock.
                FirstName = "Kia",
                LastName = "H",
                City = "Helsinki",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            var validationContext = new ValidationContext(customer, null, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(customer, validationContext, errors, true);

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
        public void CreateCustomer_CityValueTooShort_ReturnError()
        {
            string errorMessage = "Minimum of 3 characters.";
            int errorMessageCount = 0;

            Customer customer = new Customer()
            {
                CustomerId = 0, //Not required, but put in as mock.
                FirstName = "Kia",
                LastName = "Hankola",
                City = "He",
                Address = "Helsingintie 16",
                DateCreated = DateTime.Today
            };

            var validationContext = new ValidationContext(customer, null, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(customer, validationContext, errors, true);

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
        public void CreateCustomer_AddressValueTooShort_ReturnError()
        {
            string errorMessage = "Minimum of 3 characters.";
            int errorMessageCount = 0;

            Customer customer = new Customer()
            {
                CustomerId = 0, //Not required, but put in as mock.
                FirstName = "Kia",
                LastName = "Hankola",
                City = "Helsinki",
                Address = "He",
                DateCreated = DateTime.Today
            };

            var validationContext = new ValidationContext(customer, null, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(customer, validationContext, errors, true);

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
        public void CreateCustomer_PropertyValuesTooLong_ReturnErrors()
        {
            string errorMessage = "Only 100 characters allowed.";
            int errorMessageCount = 0;
            string testString = new string('a', 101);

            Customer customer = new Customer()
            {
                CustomerId = 0, //Not required, but put in as mock.
                FirstName = testString,
                LastName = testString,
                City = testString,
                Address = testString,
                DateCreated = DateTime.Today
            };

            var validationContext = new ValidationContext(customer, null, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(customer, validationContext, errors, true);

            if (errors.Count > 4 || errors.Count < 4)
                Assert.Fail();

            foreach (var error in errors)
            {
                if (errorMessage == error.ErrorMessage)
                    errorMessageCount++;
            }

            Assert.IsFalse(isValid);
            Assert.AreEqual(4, errorMessageCount);
        }
    }
}
