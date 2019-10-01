using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.DATA.Entities
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        [Required]
        public int ItemCount { get; set; }

        [Required]
        public int ItemPrice { get; set; }

        public Guid OrderId { get; set; }

        public Order Order { get; set; }

        public Guid ItemId { get; set; }

        public Item Item { get; set; }
    }
}
