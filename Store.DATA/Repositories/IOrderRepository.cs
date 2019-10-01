using Store.DATA.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.DATA.Repositories
{
    public interface IOrderRepository
    {
        Task<List<OrderDto>> GetAllAsync();

        Task<OrderDto> GetByIdAsync(Guid id);

        Task<List<OrderDto>> GetByCustomerIdAsync(Guid id);

        Task<OrderDto> GetByOrderNumberAsync(int code);

        Task<OrderDto> CreateAsync(OrderDto item);

        Task<bool> UpdateAsync(OrderDto item);

        Task<bool> DeleteAsync(Guid id);
    }
}
