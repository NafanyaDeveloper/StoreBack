using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.AUTH.Interfaces;
using Store.CORE.EF;
using Store.DATA.Converters;
using Store.DATA.Dto;
using Store.DATA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.AUTH.Services
{
    public class AuthService : IAuthService
    {
        private readonly SignInManager<Customer> _signInManager;
        private readonly UserManager<Customer> _userManager;
        private readonly StoreContext _context;
        private readonly IJwtGenerator _jwt;

        public AuthService(SignInManager<Customer> sim, UserManager<Customer> um, IJwtGenerator jwt, StoreContext context)
        {
            _signInManager = sim;
            _userManager = um;
            _jwt = jwt;
            _context = context;
        }

        public async Task<object> Login(string login, string password)
        {
            if (login == null || password == null)
                return null;

            var result = await _signInManager.PasswordSignInAsync(login, password, false, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.FindByNameAsync(login);
                return await _jwt.GenerateJwt(appUser);
            }
            return null;
        }

        public async Task<object> Register(CustomerDto item)
        {
            int count = _context.Customers.Count();
            item.Code = $"{count.ToString("0000")}-{DateTime.Now.Year}";
            while ((await _context.Customers.AsNoTracking().FirstOrDefaultAsync(x => x.Code == item.Code)) != null)
            {
                ++count;
                item.Code = $"{count.ToString("0000")}-{DateTime.Now.Year}";
            }
            var user = CustomerConverter.Convert(item);
            var result = await _userManager.CreateAsync(user, item.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                await _userManager.AddToRoleAsync(user, "manager");
                return await _jwt.GenerateJwt(user);
            }

            return null;
        }
    }



}
