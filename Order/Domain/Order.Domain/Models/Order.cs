using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Domain.Models
{
    public class Order
    {
        public Guid OrderId { get; private set; }

        public string Address { get; private set; }


        public DateTime CreatedDate { get; private set; }

        public DateTime UpdatedDate { get; private set; }
    }
}
