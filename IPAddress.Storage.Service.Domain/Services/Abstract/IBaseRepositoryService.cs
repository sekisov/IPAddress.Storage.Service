using IPAddress.Storage.Service.Domain.Models.Abstract;
using System.Linq.Expressions;

namespace IPAddress.Storage.Service.Domain.Services.Abstract
{
    public interface IBaseRepositoryService<T> where T : EntityDTO
    {
        Task<IEnumerable<T?>?> GetAllAsync();
        Task<T?> GetByIdAsync(long? id);
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetQuery();
    }
}
