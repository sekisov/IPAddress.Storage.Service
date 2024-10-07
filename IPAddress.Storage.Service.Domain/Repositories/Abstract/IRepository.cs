using IPAddress.Storage.Service.Domain.Models.Abstract;
using System.Linq.Expressions;

namespace IPAddress.Storage.Service.Domain.Repositories.Abstract
{
    public interface IRepository<T> where T : EntityDTO
    {
        Task<T> GetByIdAsync(long? id);
        T? GetById(long? id);
        IQueryable<T> GetQuery();
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
    }
}
