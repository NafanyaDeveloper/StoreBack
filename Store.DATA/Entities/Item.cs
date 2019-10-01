using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.DATA.Entities
{
    public class Item
    {
        public Guid Id { get; set; }

        [Required]
        public string Code { get; set; }

        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public string Category { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); 
    }
}
