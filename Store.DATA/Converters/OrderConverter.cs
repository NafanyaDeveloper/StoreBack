using Store.DATA.Dto;
using Store.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.DATA.Converters
{
    public static class OrderConverter
    {
        public static Order Convert(OrderDto item)
        {
            var res = new Order
            {
                Id = item.Id,
                CustomerId = item.CustomerId,
                OrderDate = item.OrderDate,
                ShipmentDate = item.ShipmentDate,
                OrderNumber = item.OrderNumber,
                Status = item.Status
            };
            if (item.OrderItems != null)
                res.OrderItems = OrderItemConverter.Convert(item.OrderItems);
            return res;
        }

        public static OrderDto Convert(Order item) =>
            new OrderDto
            {
                Id = item.Id,
                CustomerId = item.CustomerId,
                OrderDate = item.OrderDate,
                ShipmentDate = item.ShipmentDate,
                OrderNumber = item.OrderNumber,
                Status = item.Status
            };

        public static List<Order> Convert(List<OrderDto> items) =>
            items.Select(c => Convert(c)).ToList();

        public static List<OrderDto> Convert(List<Order> items) =>
            items.Select(c => Convert(c)).ToList();
    }
}
