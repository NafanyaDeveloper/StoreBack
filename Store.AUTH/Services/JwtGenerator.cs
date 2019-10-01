using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.AUTH.Interfaces;
using Store.DATA.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.AUTH.Services
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly UserManager<Customer> _userManager;
        private readonly IConfiguration _configuration;

        public JwtGenerator(UserManager<Customer> um,
            IConfiguration conf)
        {
            _userManager = um;
            _configuration = conf;
        }

        public async Task<object> GenerateJwt(Customer user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");


            claimsIdentity.AddClaims(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(30));

            var token = new JwtSecurityToken(
                _configuration["Issuer"],
                _configuration["Audience"],
                claimsIdentity.Claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);


        }
    }


}
