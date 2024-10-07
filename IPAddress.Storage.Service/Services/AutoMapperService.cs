using AutoMapper;
using IPAddress.Storage.Service.DataAccess.Models;
using IPAddress.Storage.Service.Domain.Models;
using IPAddress.Storage.Service.Models;

namespace IPAddress.Storage.Service.Services
{
    public class AutoMapperService
    {
        public IMapper GetMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>().ReverseMap();
                cfg.CreateMap<UserRequest, UserDTO>();

                cfg.CreateMap<UserIPAddress, UserIPAddressDTO>().ReverseMap();
            });

            return configuration.CreateMapper();
        }
    }
}
