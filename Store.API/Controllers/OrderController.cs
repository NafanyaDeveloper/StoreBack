using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.DATA.Dto;
using Store.DATA.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _repo;

        public OrderController(IOrderRepository repo) => _repo = repo;

        [HttpGet]
        [Produces(typeof(List<OrderDto>))]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<OrderDto>>> Get()
        {
            try
            {
                return Ok(await _repo.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpGet("{id}")]
        [Produces(typeof(OrderDto))]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<OrderDto>> Get(Guid id)
        {
            try
            {
                var res = await _repo.GetByIdAsync(id);
                if (res == null)
                    return BadRequest();
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpGet("number/{number}")]
        [Produces(typeof(OrderDto))]
        public async Task<ActionResult<OrderDto>> Get(int number)
        {
            try
            {
                var res = await _repo.GetByOrderNumberAsync(number);
                if (res == null)
                    return BadRequest();
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpGet("customer/{id}")]
        [Produces(typeof(List<OrderDto>))]
        public async Task<ActionResult<List<OrderDto>>> GetByCustomerId(Guid id)
        {
            try
            {
                var res = await _repo.GetByCustomerIdAsync(id);
                if (res == null)
                    return BadRequest();
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpDelete("{id}")]
        [Produces(typeof(bool))]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            try
            {
                return Ok(await _repo.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPost]
        [Produces(typeof(OrderDto))]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<OrderDto>> Post([FromBody] OrderDto item)
        {
            try
            {
                var res = await _repo.CreateAsync(item);
                if (res == null)
                    return BadRequest();
                return res;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }

        }

        [HttpPut]
        [Produces(typeof(bool))]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<bool>> Put([FromBody] OrderDto item)
        {
            try
            {
                return Ok(await _repo.UpdateAsync(item));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
