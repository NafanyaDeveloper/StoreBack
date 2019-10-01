using Store.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Store.AUTH.Interfaces
{
    public interface IJwtGenerator
    {
        Task<object> GenerateJwt(Customer user);
    }
}
