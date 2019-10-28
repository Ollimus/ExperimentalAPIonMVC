using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TestAPI.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [MaxLength(100)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [MaxLength(100)]
        [Required]
        [Display(Name = "First Name")]
        public string LastName { get; set; }

    }
}