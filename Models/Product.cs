using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestAPI.Models;

namespace TestAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [MinLength(3, ErrorMessage = "Minumum of 3 characters.")]
        [MaxLength(100, ErrorMessage = "Only 100 characters allowed.")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [MinLength(3, ErrorMessage = "Minumum of 3 characters.")]
        [MaxLength(40, ErrorMessage = "Only 40 characters allowed.")]
        [Required(AllowEmptyStrings = false)]
        public string Producer { get; set; }

        [MinLength(3, ErrorMessage = "Minumum of 3 characters.")]
        [MaxLength(255, ErrorMessage = "Only 255 characters allowed.")]
        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Required]
        [Range(1, 5000, ErrorMessage = "The price must be between 1 and 5000.")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 500, ErrorMessage = "Stock must be between 1 and 500.")]
        public int Stock { get; set; }
    }
}