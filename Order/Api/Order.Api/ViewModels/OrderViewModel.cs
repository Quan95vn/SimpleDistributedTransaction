using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Api.ViewModels
{
    public class OrderViewModel
    {
        public Guid OrderId { get; set; }

        public string Address { get; set; }

        public DateTime CreatedDate => DateTime.Now;
    }
}
