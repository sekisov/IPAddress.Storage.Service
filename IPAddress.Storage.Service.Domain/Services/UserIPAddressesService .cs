using FluentValidation;
using IPAddress.Storage.Service.Domain.Models;
using IPAddress.Storage.Service.Domain.Repositories.Abstract;
using IPAddress.Storage.Service.Domain.Services.Abstract;

namespace IPAddress.Storage.Service.Domain.Services
{
    public class UserIPAddressesService : BaseEditableRepositoryService<UserIPAddressDTO>, IUserIPAddressesService
    {
        private readonly IEditableRepository<UserIPAddressDTO> _repository;
        public UserIPAddressesService(IEditableRepository<UserIPAddressDTO> repository,
                            IValidator<UserIPAddressDTO> validator)
                            : base(repository, validator)
        {
            _repository = repository;
        }


        public override async Task<UserIPAddressDTO?> CreateAsync(UserIPAddressDTO? item)
        {
            if (item == null)
            {
                return null;
            }

            var createdItem = await base.CreateAsync(item);
            return createdItem;
        }

        public IEnumerable<UserIPAddressDTO>? GetByAddress(string? address)
        {
            if (string.IsNullOrEmpty(address))
            {
                return null;
            }
            return _repository.GetQuery().Where(x => x.Address == address);
        }
    }
}
