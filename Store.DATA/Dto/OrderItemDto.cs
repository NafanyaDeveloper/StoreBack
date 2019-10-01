﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Store.DATA.Dto
{
    public class OrderItemDto
    {
        public Guid Id { get; set; }

        [Required]
        public int ItemCount { get; set; }

        [Required]
        public int ItemPrice { get; set; }

        public Guid OrderId { get; set; }

        public OrderDto Order { get; set; }

        public int OrderNumber { get; set; }

        public string ItemName { get; set; }

        public Guid ItemId { get; set; }

        public ItemDto Item { get; set; }
    }
}
