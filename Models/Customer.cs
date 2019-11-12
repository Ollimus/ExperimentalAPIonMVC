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

        [MinLength(3, ErrorMessage = "Minumum of 3 characters.")]
        [MaxLength(100, ErrorMessage = "Only 100 characters allowed.")]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MinLength(3, ErrorMessage = "Minumum of 3 characters.")]
        [MaxLength(100, ErrorMessage = "Only 100 characters allowed.")]
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [MinLength(3, ErrorMessage = "Minumum of 3 characters.")]
        [MaxLength(100, ErrorMessage = "Only 100 characters allowed.")]
        [Required(AllowEmptyStrings = false)]
        public string City { get; set; }

        [MinLength(3, ErrorMessage = "Minumum of 3 characters.")]
        [MaxLength(100, ErrorMessage = "Only 100 characters allowed.")]
        [Required(AllowEmptyStrings = false)]
        public string Address { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}