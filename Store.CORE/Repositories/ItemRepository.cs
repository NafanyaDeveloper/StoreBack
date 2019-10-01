using Microsoft.EntityFrameworkCore;
using Store.CORE.EF;
using Store.DATA.Converters;
using Store.DATA.Dto;
using Store.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.CORE.Repositories
{
    public class ItemRepository: IItemRepository
    {
        private readonly StoreContext _context;

        public ItemRepository(StoreContext context) => _context = context;

        public async Task<ItemDto> CreateAsync(ItemDto item)
        {
            try
            {
                item.Code = GenerateCode();
                var result = await _context.Items.AddAsync(ItemConverter.Convert(item));
                await _context.SaveChangesAsync();
                return ItemConverter.Convert(result.Entity);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var item = await _context.Items.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (item == null)
                    return false;
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<ItemDto>> GetAllAsync()
        {
            try
            {
                return await _context.Items.AsNoTracking().Select(x => new ItemDto
                {
                    Category = x.Category,
                    Code = x.Code,
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price
                }).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ItemDto> GetByCodeAsync(string code)
        {
            try
            {
                return await _context.Items.AsNoTracking().Select(x => new ItemDto
                {
                    Category = x.Category,
                    Code = x.Code,
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    OrderItems = OrderItemConverter.Convert(x.OrderItems)
                }).FirstOrDefaultAsync(y => y.Code == code);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ItemDto> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Items.AsNoTracking().Select(x => new ItemDto
                {
                    Category = x.Category,
                    Code = x.Code,
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    OrderItems = OrderItemConverter.Convert(x.OrderItems)
                }).FirstOrDefaultAsync(y => y.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(ItemDto item)
        {
            try
            {
                var it = await _context.Items.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);
                if (it == null)
                    return false;
                item.Code = it.Code;
                _context.Items.Update(ItemConverter.Convert(item));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GenerateCode()
        {
            string minute = DateTime.Now.Minute.ToString("00");
            string sec = DateTime.Now.Second.ToString("00");
            string milisec = DateTime.Now.Millisecond.ToString("0000");
            Random rand = new Random();
            char first = (char)rand.Next('A', 'Z' + 1);
            char second = (char)rand.Next('A', 'Z' + 1);
            return $"{minute}-{milisec}-{first.ToString()}{second.ToString()}{sec}";
        }
    }
}
