using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store.CORE.EF;
using Store.DATA.Dto;
using Store.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Store.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "customer")]
    public class AccountController: Controller
    {
        private readonly IOrderRepository _orderRepo;
        private readonly StoreContext _context;

        public AccountController(IOrderRepository orderRepo, StoreContext context)
        {
            _orderRepo = orderRepo;
            _context = context;
        }

        [HttpGet]
        [Produces(typeof(List<OrderDto>))]
        public async Task<ActionResult<List<OrderDto>>> GetOrders()
        {
            try
            {
                var id = await GetUserId();
                if(id != Guid.Empty)
                    return Ok(await _orderRepo.GetByCustomerIdAsync(id));
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpDelete("{id}")]
        [Produces(typeof(bool))]
        public async Task<ActionResult<bool>> DeleteOrder(Guid id)
        {
            try
            {
                var i = await GetUserId();
                if (i != Guid.Empty)
                {
                    var order = await _orderRepo.GetByIdAsync(id);
                    if (order.CustomerId == i && order.Status == "Новый")
                        return Ok(await _orderRepo.DeleteAsync(id));
                }
                    
                return false;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPost]
        [Produces(typeof(bool))]
        public async Task<ActionResult<bool>> AddOrder([FromBody] OrderDto item)
        {
            try
            {
                var id = await GetUserId();
                if (id != Guid.Empty)
                {
                    item.CustomerId = id;
                    return Ok(await _orderRepo.CreateAsync(item));
                }
                return false;
                    
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        private async Task<Guid> GetUserId()
        {
            try
            {
                var i = HttpContext.User
                    .FindFirst(ClaimTypes.NameIdentifier).Value;
                var res = await _context.Customers.AsNoTracking().Select(x => new
                {
                    login = x.UserName,
                    id = x.Id
                }).FirstOrDefaultAsync(y => y.login == i);
                return res.id;
            }
            catch (Exception)
            {
                return Guid.Empty;
            }
        }
    }
}
