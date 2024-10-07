using AutoMapper;
using AutoMapper.QueryableExtensions;
using IPAddress.Storage.Service.DataAccess.Models.Abstract;
using IPAddress.Storage.Service.Domain.Models.Abstract;
using IPAddress.Storage.Service.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IPAddress.Storage.Service.DataAccess.Repositories
{
    public class BaseRepository<T, E> : IRepository<T> where T : EntityDTO where E : class, IEntity
    {
        protected readonly DataContext _dataContext;
        protected readonly IMapper _mapper;
        public BaseRepository(DataContext dataContext
            , IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<T> GetByIdAsync(long? id)
        {
            if (id is null)
            {
                throw new ArgumentNullException(nameof(id));
            } 

            return await _dataContext.Set<E>().ProjectTo<T>(_mapper.ConfigurationProvider).FirstAsync(x => x.Id == id);
        }

        public T? GetById(long? id)
        {
            return GetQuery().FirstOrDefault(x => x.Id == id);
        } 

        public IQueryable<T> GetQuery()
        {
            return _dataContext.Set<E>().ProjectTo<T>(_mapper.ConfigurationProvider);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await GetQuery().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await GetQuery().Where(predicate).ToListAsync();
        }
    }
}