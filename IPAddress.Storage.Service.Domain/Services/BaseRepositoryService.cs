using IPAddress.Storage.Service.Domain.Models.Abstract;
using IPAddress.Storage.Service.Domain.Repositories.Abstract;
using IPAddress.Storage.Service.Domain.Services.Abstract;
using System.Linq.Expressions;

namespace IPAddress.Storage.Service.Domain.Services
{
    public class BaseRepositoryService<T, E> : IBaseRepositoryService<T> where T : EntityDTO where E : IRepository<T>
    {
        protected readonly E Repository;

        protected BaseRepositoryService(E repository)
        {
            Repository = repository;
        }

        public virtual async Task<IEnumerable<T?>?> GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }

        public virtual async Task<T?> GetByIdAsync(long? id)
        {
            return id == null || id == 0 ? null : await Repository.GetByIdAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await Repository.GetAsync(predicate);
        }

        public virtual IQueryable<T> GetQuery()
        {
            return Repository.GetQuery();
        }
    }
}
