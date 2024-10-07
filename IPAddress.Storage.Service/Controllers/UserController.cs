using AutoMapper;
using FluentValidation;
using IPAddress.Storage.Service.Domain.Models;
using IPAddress.Storage.Service.Domain.Services.Abstract;
using IPAddress.Storage.Service.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace IPAddress.Storage.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsersService _service;

        public UserController(IUsersService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("Get")]
        [Authorize]
        public async Task<ActionResult<UserDTO?>> GetAsync(long id)
        {
            var time = await _service.GetByIdAsync(id);
            return Ok(time);
        }

        [AllowAnonymous]
        [HttpPost("Create")]
        public async Task<ActionResult> CreateAsync(UserRequest request)
        {
            try
            {
                var newItem = _mapper.Map<UserDTO>(request);

                var item = await _service.CreateAsync(newItem);

                if (item == null)
                {
                    return BadRequest();
                }

                var result = new JsonObject { { "id", item.Id } };

                return Created(nameof(item), result);
            }
            catch (ValidationException ex)
            {
                return BadRequest(string.Join("\n", ex.Errors.Select(x => x.ErrorMessage)));
            }
        }
    }
}
