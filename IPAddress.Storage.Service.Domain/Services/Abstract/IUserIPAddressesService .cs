using IPAddress.Storage.Service.Domain.Models;

namespace IPAddress.Storage.Service.Domain.Services.Abstract
{
    public interface IUserIPAddressesService : IBaseEditableRepositoryService<UserIPAddressDTO>
    {
        IEnumerable<UserIPAddressDTO>? GetByAddress(string? address);
    }
}
