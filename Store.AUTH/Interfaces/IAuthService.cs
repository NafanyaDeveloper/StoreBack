using Store.DATA.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.AUTH.Interfaces
{
    public interface IAuthService
    {
        Task<object> Login(string login, string password);

        Task<object> Register(CustomerDto item);
    }
}
