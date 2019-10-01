using Store.DATA.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.DATA.Repositories
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItemDto>> GetAllAsync();

        Task<OrderItemDto> GetByIdAsync(Guid id);

        Task<List<OrderItemDto>> GetByOrderIdAsync(Guid id);

        Task<OrderItemDto> CreateAsync(OrderItemDto item);

        Task<bool> UpdateAsync(OrderItemDto item);

        Task<bool> DeleteAsync(Guid id);
    }
}
