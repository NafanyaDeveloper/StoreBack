using Store.DATA.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.DATA.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDto>> GetAllAsync();

        Task<CustomerDto> GetByIdAsync(Guid id);

        Task<CustomerDto> GetByCodeAsync(string code);

        Task<List<CustomerDto>> GetByUserIdAsync(Guid id);

        Task<CustomerDto> CreateAsync(CustomerDto item);

        Task<bool> UpdateAsync(CustomerDto item);

        Task<bool> DeleteAsync(Guid id);
    }
}
