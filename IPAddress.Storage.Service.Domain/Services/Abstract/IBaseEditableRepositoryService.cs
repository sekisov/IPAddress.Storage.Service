using IPAddress.Storage.Service.Domain.Models.Abstract;

namespace IPAddress.Storage.Service.Domain.Services.Abstract
{
    public interface IBaseEditableRepositoryService<T> : IBaseRepositoryService<T> where T : EditableEntityDTO
    {
        Task<T?> UpdateAsync(T item);
        Task<T?> CreateAsync(T item);
        Task DeleteAsync(long id);
    }
}
