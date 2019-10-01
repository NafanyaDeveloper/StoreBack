using Store.DATA.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.DATA.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserDto>> GetAllAsync();

        Task<UserDto> GetByIdAsync(Guid id);

        Task<UserDto> CreateAsync(UserDto item);

        Task<bool> UpdateAsync(UserDto item);

        Task<bool> DeleteAsync(Guid id);
    }
}
