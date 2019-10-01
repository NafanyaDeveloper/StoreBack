using Store.DATA.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.DATA.Repositories
{
    public interface IItemRepository
    {
        Task<List<ItemDto>> GetAllAsync();

        Task<ItemDto> GetByIdAsync(Guid id);

        Task<ItemDto> GetByCodeAsync(string code);

        Task<ItemDto> CreateAsync(ItemDto item);

        Task<bool> UpdateAsync(ItemDto item);

        Task<bool> DeleteAsync(Guid id);
    }
}
