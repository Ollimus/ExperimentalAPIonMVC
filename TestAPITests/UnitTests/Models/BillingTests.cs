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
    public class BillingTests
    {
        [TestMethod]
        public void CreateBilling_CorrectDetails_ValidationSuccess()
        {
            Billing billing = new Billing()
            {

            };

            var validationContext = new ValidationContext(billing, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(billing, validationContext, errors, true);

            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void CreateBilling_NoProperties_ValidationFailure()
        {
            Billing billing = new Billing();

            var validationContext = new ValidationContext(billing, null, null);
            var errors = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(billing, validationContext, errors, true);

            Assert.IsFalse(isValid);
        }
    }
}
