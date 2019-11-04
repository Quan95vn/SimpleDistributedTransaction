using System;

namespace Order.Api.ViewModels
{
    /// <summary>
    /// Class OrderDetailViewModel
    /// </summary>
    public class OrderDetailViewModel
    {
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}