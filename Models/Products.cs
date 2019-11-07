using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TestAPI.Models;

namespace TestAPI.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }

        [MinLength(5)]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [MinLength(5)]
        [MaxLength(255)]
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 5000)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Stock { get; set; }
    }
}