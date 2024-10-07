using FluentValidation;
using IPAddress.Storage.Service.Domain.Models;
using IPAddress.Storage.Service.Domain.Repositories.Abstract;
using IPAddress.Storage.Service.Domain.Services.Abstract;

namespace IPAddress.Storage.Service.Domain.Services
{
    public class UsersService : BaseEditableRepositoryService<UserDTO>, IUsersService
    {
        private readonly IEditableRepository<UserDTO> _repository;
        public UsersService(IEditableRepository<UserDTO> repository,
                            IValidator<UserDTO> validator)
                            : base(repository, validator)
        {
            _repository = repository;
        }

        public override async Task<UserDTO?> CreateAsync(UserDTO? item)
        {
            if (item == null)
            {
                return null;
            }

            var createdItem = await base.CreateAsync(item);
            return createdItem;
        }
    }
}
