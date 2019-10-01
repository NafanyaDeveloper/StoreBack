﻿using Microsoft.EntityFrameworkCore;
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
    public class OrderRepository: IOrderRepository
    {
        private readonly StoreContext _context;

        public OrderRepository(StoreContext context) => _context = context;

        public async Task<OrderDto> CreateAsync(OrderDto item)
        {
            try
            {
                item.OrderDate = DateTime.Now;
                item.Status = "Новый";
                Random rnd = new Random();
                item.OrderNumber = rnd.Next(100000, 999999);
                if (item.OrderItems != null)
                    foreach(var i in item.OrderItems)
                    {
                        var it = await _context.Items.AsNoTracking().FirstOrDefaultAsync(x => x.Id == i.ItemId);
                        if (it == null)
                            return null;
                        i.ItemPrice = it.Price;
                    }
                var result = await _context.Orders.AddAsync(OrderConverter.Convert(item));
                await _context.SaveChangesAsync();
                return OrderConverter.Convert(result.Entity);
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
                var order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (order == null)
                    return false;
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<OrderDto>> GetAllAsync()
        {
            try
            {
                return await _context.Orders.AsNoTracking().Select(x => new OrderDto
                {
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    OrderNumber = x.OrderNumber,
                    ShipmentDate = x.ShipmentDate,
                    Status = x.Status
                }).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OrderDto> GetByOrderNumberAsync(int code)
        {
            try
            {
                return await _context.Orders.AsNoTracking().Select(x => new OrderDto
                {
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    OrderNumber = x.OrderNumber,
                    ShipmentDate = x.ShipmentDate,
                    Status = x.Status,
                    Customer = CustomerConverter.Convert(x.Customer),
                    OrderItems = OrderItemConverter.Convert(x.OrderItems)
                }).FirstOrDefaultAsync(y => y.OrderNumber == code);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<OrderDto> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Orders.AsNoTracking().Select(x => new OrderDto
                {
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    OrderNumber = x.OrderNumber,
                    ShipmentDate = x.ShipmentDate,
                    Status = x.Status,
                    Customer = CustomerConverter.Convert(x.Customer),
                    OrderItems = OrderItemConverter.Convert(x.OrderItems)
                }).FirstOrDefaultAsync(y => y.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(OrderDto item)
        {
            try
            {
                var order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);
                if (order == null)
                    return false;
                item.OrderNumber = order.OrderNumber;
                item.OrderDate = order.OrderDate;
                _context.Orders.Update(OrderConverter.Convert(item));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<OrderDto>> GetByCustomerIdAsync(Guid id)
        {
            try
            {
                return await _context.Orders.AsNoTracking().Select(x => new OrderDto
                {
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    OrderNumber = x.OrderNumber,
                    ShipmentDate = x.ShipmentDate,
                    Status = x.Status,
                    Customer = CustomerConverter.Convert(x.Customer),
                    OrderItems = OrderItemConverter.Convert(x.OrderItems)
                }).Where(y => y.CustomerId == id).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
