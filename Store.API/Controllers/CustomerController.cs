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
    [Authorize(Roles = "manager")]
    public class CustomerController: Controller
    {
        private readonly ICustomerRepository _repo;

        public CustomerController(ICustomerRepository repo) => _repo = repo;

        [HttpGet]
        [Produces(typeof(List<CustomerDto>))]
        public async Task<ActionResult<List<CustomerDto>>> Get()
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
        [Produces(typeof(CustomerDto))]
        public async Task<ActionResult<CustomerDto>> Get(Guid id)
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
        [Produces(typeof(CustomerDto))]
        public async Task<ActionResult<CustomerDto>> Get(string code)
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

        [HttpGet("user/{id}")]
        [Produces(typeof(List<CustomerDto>))]
        public async Task<ActionResult<List<CustomerDto>>> GetByUserId(Guid id)
        {
            try
            {
                var res = await _repo.GetByUserIdAsync(id);
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
        [Produces(typeof(CustomerDto))]
        public async Task<ActionResult<CustomerDto>> Post([FromBody] CustomerDto item)
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
        public async Task<ActionResult<bool>> Put([FromBody] CustomerDto item)
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
