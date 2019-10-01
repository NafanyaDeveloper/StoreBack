using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.DATA.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime? ShipmentDate { get; set; } = null;

        [Required]
        public int OrderNumber { get; set; }

        [Required]
        public string Status { get; set; }

        public Guid CustomerId { get; set; }

        public Customer Customer { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
