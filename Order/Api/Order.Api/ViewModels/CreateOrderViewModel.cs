using System;

namespace Order.Api.ViewModels
{
    /// <summary>
    /// Create OrderViewModel
    /// </summary>
    public class CreateOrderViewModel
    {
        public Guid OrderId { get; set; }

        public string Address { get; set; }

        public OrderDetailViewModel OrderDetails { get; set; }

        public DateTime CreatedDate => DateTime.Now;
    }
}