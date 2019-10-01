using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.CORE.EF;
using Store.DATA.Converters;
using Store.DATA.Dto;
using Store.DATA.Entities;
using Store.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.CORE.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly StoreContext _context;
        private readonly UserManager<Customer> _uManager;

        public CustomerRepository(StoreContext context, UserManager<Customer> um)
        {
            _context = context;
            _uManager = um;
        }

        public async Task<CustomerDto> CreateAsync(CustomerDto item)
        {
            try
            {
                int count = _context.Customers.Count();
                item.Code = $"{count.ToString("0000")}-{DateTime.Now.Year}";
                while((await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Code == item.Code)) != null)
                {
                    ++count;
                    item.Code = $"{count.ToString("0000")}-{DateTime.Now.Year}";
                }
                var result = await _context.Customers.AddAsync(CustomerConverter.Convert(item));
                await _context.SaveChangesAsync();
                return CustomerConverter.Convert(result.Entity);
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
                var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (customer == null)
                    return false;
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<CustomerDto>> GetAllAsync()
        {
            try
            {
                var role = await _context.Roles.AsNoTracking().Select(x => new { id = x.Id, NormalizedName = x.NormalizedName })
                    .FirstOrDefaultAsync(y => y.NormalizedName == "CUSTOMER");
                var ids = await _context.UserRoles.AsNoTracking().Where(x => x.RoleId == role.id).Select(y => y.UserId).ToListAsync();
                return await _context.Customers.AsNoTracking().Where(y => ids.Contains(y.Id)).Select(x => new CustomerDto
                {
                    Address = x.Address,
                    Code = x.Code,
                    Discount = x.Discount,
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.UserId,
                    UserName = x.UserName,
                    StoreUserName = x.User.Surname + " " + x.User.Name
                }).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CustomerDto> GetByCodeAsync(string code)
        {
            try
            {
                return await _context.Customers.AsNoTracking().Select(x => new CustomerDto
                {
                    Address = x.Address,
                    Code = x.Code,
                    Discount = x.Discount,
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.UserId,
                    UserName = x.UserName,
                    StoreUserName = x.User.Surname + " " + x.User.Name,
                    Orders = OrderConverter.Convert(x.Orders),
                    User = UserConverter.Convert(x.User)
                }).FirstOrDefaultAsync(y => y.Code == code);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<CustomerDto> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Customers.AsNoTracking().Select(x => new CustomerDto
                {
                    Address = x.Address,
                    Code = x.Code,
                    Discount = x.Discount,
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.UserId,
                    UserName = x.UserName,
                    StoreUserName = x.User.Surname + " " + x.User.Name,
                    Orders = OrderConverter.Convert(x.Orders),
                    User = UserConverter.Convert(x.User)
                }).FirstOrDefaultAsync(y => y.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<CustomerDto>> GetByUserIdAsync(Guid id)
        {
            try
            {
                return await _context.Customers.AsNoTracking().Select(x => new CustomerDto
                {
                    Address = x.Address,
                    Code = x.Code,
                    Discount = x.Discount,
                    Id = x.Id,
                    Name = x.Name,
                    UserId = x.UserId,
                    UserName = x.UserName,
                    StoreUserName = x.User.Surname + x.User.Name,
                    Orders = OrderConverter.Convert(x.Orders),
                    User = UserConverter.Convert(x.User)
                }).Where(y => y.UserId == id).ToListAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> UpdateAsync(CustomerDto item)
        {
            try
            {
                var customer = await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);
                if (customer == null)
                    return false;
                //item.Code = customer.Code;
                //var a = CustomerConverter.Convert(item);
                customer.Address = item.Address;
                customer.Discount = item.Discount;
                customer.Name = item.Name;
                customer.UserId = item.UserId;
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
