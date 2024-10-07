using FluentValidation;
using FluentValidation.Results;
using IPAddress.Storage.Service.Domain.Models.Abstract;
using IPAddress.Storage.Service.Domain.Repositories.Abstract;
using IPAddress.Storage.Service.Domain.Services.Abstract;

namespace IPAddress.Storage.Service.Domain.Services
{
    public abstract class BaseEditableRepositoryService<T> : BaseRepositoryService<T, IEditableRepository<T>>, IBaseEditableRepositoryService<T> where T : EditableEntityDTO
    {
        private readonly IValidator<T> _validator;

        protected BaseEditableRepositoryService(IEditableRepository<T> repository,
            IValidator<T> validator)
            : base(repository)
        {
            _validator = validator;
        }

        public virtual async Task<T?> UpdateAsync(T? item)
        {
            if (item == null)
            {
                return null;
            }

            await _validator.ValidateAsync(item, options =>
            {
                options.ThrowOnFailures();
                options.IncludeRuleSets("Update");
            });

            var oldItem = await Repository.GetByIdAsync(item.Id);

            item.CreatedBy = oldItem.CreatedBy;
            item.CreatedAt = oldItem.CreatedAt;
            item.Id = oldItem.Id;

            item.UpdatedAt = DateTime.Now;
            item.UpdatedBy = "Service";

            var updatedItem = await Repository.UpdateAsync(item);
            return updatedItem;
        }

        public virtual async Task<T?> CreateAsync(T? item)
        {
            if (item == null)
            {
                return null;
            }

            item.CreatedAt = DateTime.Now;
            item.CreatedBy = "Service";
            item.UpdatedAt = DateTime.Now;
            item.UpdatedBy = "Service";

            await _validator.ValidateAsync(item, options =>
            {
                options.ThrowOnFailures();
                options.IncludeRuleSets("Create");
            });

            var createdItem = await Repository.CreateAsync(item);
            return createdItem;
        }

        public async Task DeleteAsync(long id)
        {
            if (id == 0)
            {
                return;
            }

            var item = await Repository.GetByIdAsync(id);

            if (item == null)
            {
                throw new ValidationException(
                    new List<ValidationFailure>()
                    {
                        new ("Delete item", Resources.Errors.ItemNotFound)
                    });
            }

            await _validator.ValidateAsync(item, options =>
            {
                options.ThrowOnFailures();
                options.IncludeRuleSets("Delete");
            });

            await Repository.DeleteAsync(item);
        }

    }
}
