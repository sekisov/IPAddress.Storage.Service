using AutoMapper;
using FluentValidation;
using IPAddress.Storage.Service.Domain.Models;
using IPAddress.Storage.Service.Domain.Services.Abstract;
using IPAddress.Storage.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace IPAddress.Storage.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserIPAddressController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserIPAddressesService _serviceIPAddress;
        private readonly IUsersService _usersService;

        public UserIPAddressController(IUserIPAddressesService service, IUsersService usersService, IMapper mapper)
        {
            _serviceIPAddress = service;
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateAsync(UserIPAddressRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }

                var user = _usersService.GetQuery().Where(x => x.Id == request.UserId).FirstOrDefault();
                if (user == null)
                {
                    _usersService.CreateAsync(new UserDTO()
                    {
                        Username = request.Username,
                        UserIPAddresses = new List<UserIPAddressDTO>()
                    {
                        new UserIPAddressDTO() {Address =  request.IPAddress}
                    }
                    }).GetAwaiter().GetResult();
                }
                else
                {
                    user.Username = "TEsST";
                    var ip = await _serviceIPAddress.CreateAsync(new UserIPAddressDTO() { Address = request.IPAddress });
                    user.UserIPAddresses.Add(ip);
                    await _usersService.UpdateAsync(user);
                }
                return Created();
            }
            catch (ValidationException ex)
            {
                return BadRequest(string.Join("\n", ex.Errors.Select(x => x.ErrorMessage)));
            }
        }

        [HttpGet("Get")]
        public async Task<ActionResult<UserIPAddressDTO?>> GetAsync(long id)
        {
            var time = await _serviceIPAddress.GetByIdAsync(id);
            return Ok(time);
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<UserIPAddressDTO>?>?> GetAllAsync()
        {
            var items = await _serviceIPAddress.GetAllAsync();

            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPut("Update")]
        public async Task<ActionResult<UserIPAddressDTO>> UpdateAsync(UserIPAddressDTO request)
        {
            try
            {
                var item = await _serviceIPAddress.UpdateAsync(request);

                if (item == null)
                {
                    return BadRequest();
                }

                return Ok(item);
            }
            catch (ValidationException ex)
            {
                return BadRequest(string.Join("\n", ex.Errors.Select(x => x.ErrorMessage)));
            }
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteAsync(long id)
        {
            try
            {
                await _serviceIPAddress.DeleteAsync(id);

                if (await _serviceIPAddress.GetByIdAsync(id) != null)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (ValidationException ex)
            {
                return BadRequest(string.Join("\n", ex.Errors.Select(x => x.ErrorMessage)));
            }
        }


    }
}
