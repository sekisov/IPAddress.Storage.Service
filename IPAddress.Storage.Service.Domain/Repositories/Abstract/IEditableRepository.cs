
using IPAddress.Storage.Service.Domain.Models.Abstract;

namespace IPAddress.Storage.Service.Domain.Repositories.Abstract
{
    public interface IEditableRepository<T> : IRepository<T> where T : EditableEntityDTO
    {
        Task<T> UpdateAsync(T entity);
        Task<T> CreateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
