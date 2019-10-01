using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.API.ViewModels;
using Store.AUTH.Interfaces;
using Store.DATA.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.API.Controllers
{
    [Route("api/[action]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _auth;


        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost]
        [AllowAnonymous]
        [Produces(typeof(object))]
        public async Task<ActionResult<object>> Login([FromBody] LoginViewModel form)
        {
            try
            {
                var result = await _auth.Login(form.Login, form.Password);
                if (result == null)
                    return BadRequest();
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [Produces(typeof(object))]
        public async Task<ActionResult<object>> Register([FromBody] CustomerDto item)
        {
            try
            {
                var result = await _auth.Register(item);
                if (result == null)
                    return BadRequest();
                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }
    }

}
