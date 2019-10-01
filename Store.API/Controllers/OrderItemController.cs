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
    public class OrderItemController : Controller
    {
        private readonly IOrderItemRepository _repo;

        public OrderItemController(IOrderItemRepository repo) => _repo = repo;

        [HttpGet]
        [Produces(typeof(List<OrderItemDto>))]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<List<OrderItemDto>>> Get()
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
        [Produces(typeof(OrderItemDto))]
        public async Task<ActionResult<OrderItemDto>> Get(Guid id)
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

        [HttpGet("order/{id}")]
        [Produces(typeof(List<OrderItemDto>))]
        public async Task<ActionResult<List<OrderItemDto>>> GetByOrderId(Guid id)
        {
            try
            {
                var res = await _repo.GetByOrderIdAsync(id);
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
        [Produces(typeof(OrderItemDto))]
        public async Task<ActionResult<OrderItemDto>> Post([FromBody] OrderItemDto item)
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
        public async Task<ActionResult<bool>> Put([FromBody] OrderItemDto item)
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
