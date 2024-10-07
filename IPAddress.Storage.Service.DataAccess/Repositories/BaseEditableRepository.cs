using AutoMapper;
using IPAddress.Storage.Service.DataAccess.Models.Abstract;
using IPAddress.Storage.Service.Domain.Models.Abstract;
using IPAddress.Storage.Service.Domain.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace IPAddress.Storage.Service.DataAccess.Repositories
{
    public class BaseEditableRepository<T, E> : BaseRepository<T, E>, IEditableRepository<T> where T : EditableEntityDTO where E : class, IEditableEntity
    {
        public BaseEditableRepository(DataContext dataContext
            , IMapper mapper) : base(dataContext, mapper)
        { }

        public async Task<T> CreateAsync(T dto)
        {
            var item = _mapper.Map<T, E>(dto);
            await _dataContext.Set<E>().AddAsync(item);
            await _dataContext.SaveChangesAsync();
            return await GetByIdAsync(item.Id);
        }

        public async Task<T> UpdateAsync(T dto)
        {
            var item = _mapper.Map<T, E>(dto);

            _dataContext.Entry(await _dataContext.Set<E>().FirstAsync(x => x.Id == dto.Id)).CurrentValues.SetValues(item);
            await _dataContext.SaveChangesAsync();
            return await GetByIdAsync(item.Id);
        }

        public async Task DeleteAsync(T dto)
        {
            _dataContext.Set<E>().Remove(await _dataContext.Set<E>().FirstAsync(x => x.Id == dto.Id));
            await _dataContext.SaveChangesAsync();
        }
    }
}
