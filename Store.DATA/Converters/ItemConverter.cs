using Store.DATA.Dto;
using Store.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Store.DATA.Converters
{
    public static class ItemConverter
    {
        public static Item Convert(ItemDto item) =>
            new Item
            {
                Id = item.Id,
                Name = item.Name,
                Category = item.Category,
                Code = item.Code,
                Price = item.Price
            };

        public static ItemDto Convert(Item item) =>
            new ItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Category = item.Category,
                Code = item.Code,
                Price = item.Price
            };

        public static List<Item> Convert(List<ItemDto> items) =>
            items.Select(c => Convert(c)).ToList();

        public static List<ItemDto> Convert(List<Item> items) =>
            items.Select(c => Convert(c)).ToList();
    }
}
