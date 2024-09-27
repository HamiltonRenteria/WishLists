using Domain.Entities;
using Infrastructure.Commons.Bases.Request;
using Infrastructure.Helpers;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using Utilities.Static;

namespace Infrastructure.Persistence.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DvpDbContext _context;
        private readonly DbSet<T> _entity;

        public GenericRepository(DvpDbContext context)
        {
            _context = context;
            _entity = _context.Set<T>();
        }

        public async Task<int> CreateAsync(T entity)
        {
            await _context.AddAsync(entity);

            await _context.SaveChangesAsync();

            int idProperty = (int)entity.GetType().GetProperty("Id")?.GetValue(entity, null)!;

            return idProperty;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            T entity = await GetByIdAsync(id);

            entity!.State = Convert.ToBoolean(StateTypes.Inactive);

            _context.Update(entity);

            _context.Update(entity);

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var getAll = await _entity
                .Where(x => x.State! == Convert.ToBoolean(Convert.ToInt32(StateTypes.Active)))
                .AsNoTracking()
                .ToListAsync();

            return getAll;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var getById = await _entity!.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

            return getById!;
        }

        public IQueryable<T> GetEntityQuery(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _entity;

            if (filter is not null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Update(entity);

            var recordsAffected = await _context.SaveChangesAsync();
            return recordsAffected > 0;
        }

        public IQueryable<TDTO> Ordering<TDTO>(BasePaginationRequest request, IQueryable<TDTO> queryable, bool pagination = false) where TDTO : class
        {
            IQueryable<TDTO> queryDto = request.Order == "desc" ? queryable.OrderBy($"{request.Sort} desc") : queryable.OrderBy($"{request.Sort} asc");

            if (pagination) queryDto = queryDto.Paginate(request);

            return queryDto;
        }
    }
}
