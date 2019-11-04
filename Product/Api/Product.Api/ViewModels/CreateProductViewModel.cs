using System;
using System.ComponentModel.DataAnnotations;

namespace Product.Api.ViewModels
{
    /// <summary>
    /// Create ProductViewModel
    /// </summary>
    public class CreateProductViewModel
    {
        public Guid? ProductId { get; set; }

        [Required(ErrorMessage = "The Name field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Price field is required")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "The QuantityInStock field is required")]
        public int QuantityInStock { get; set; }

        public DateTime CreatedDate => DateTime.Now;
    }
}