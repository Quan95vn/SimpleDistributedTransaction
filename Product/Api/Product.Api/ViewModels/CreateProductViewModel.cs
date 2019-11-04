using System;

namespace Product.Api.ViewModels
{
    /// <summary>
    /// Create ProductViewModel
    /// </summary>
    public class CreateProductViewModel
    {
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public DateTime CreatedDate => DateTime.Now;
    }
}