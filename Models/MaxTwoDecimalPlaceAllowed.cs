using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
    public class MaxTwoDecimalPlaceAllowed : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var customer = (Product)validationContext.ObjectInstance;

            bool isTheValueRounded = (customer.Price == Math.Round(customer.Price, 2));

            if (isTheValueRounded)
                return ValidationResult.Success;

            return new ValidationResult("");
        }
    }
}