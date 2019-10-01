using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.DATA.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime? ShipmentDate { get; set; }

        [Required]
        public int OrderNumber { get; set; }

        [Required]
        public string Status { get; set; }

        public Guid CustomerId { get; set; }

        public CustomerDto Customer { get; set; }

        public string CustomerName { get; set; }

        public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
