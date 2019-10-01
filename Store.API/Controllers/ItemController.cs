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
    public class ItemController : Controller
    {
        private readonly IItemRepository _repo;

        public ItemController(IItemRepository repo) => _repo = repo;

        [HttpGet]
        [Produces(typeof(List<ItemDto>))]
        public async Task<ActionResult<List<ItemDto>>> Get()
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
        [Produces(typeof(ItemDto))]
        public async Task<ActionResult<ItemDto>> Get(Guid id)
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

        [HttpGet("code/{code}")]
        [Produces(typeof(ItemDto))]
        public async Task<ActionResult<ItemDto>> Get(string code)
        {
            try
            {
                var res = await _repo.GetByCodeAsync(code);
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
        [Produces(typeof(ItemDto))]
        [Authorize(Roles = "manager")]
        public async Task<ActionResult<ItemDto>> Post([FromBody] ItemDto item)
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
        public async Task<ActionResult<bool>> Put([FromBody] ItemDto item)
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
