using System;
using System.Collections.Generic;

namespace Order.Api.ViewModels
{
    /// <summary>
    /// Create OrderViewModel
    /// </summary>
    public class CreateOrderViewModel
    {
        public Guid OrderId { get; set; }

        public string Address { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }

        public DateTime CreatedDate => DateTime.Now;
    }
}