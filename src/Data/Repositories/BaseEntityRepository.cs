using System.Data;
using System.Linq.Expressions;
using Data.Context;
using Domain.Entities.Shared;
using Domain.Interfaces;
using Domain.Types;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BaseEntityRepository<T> : IEntityRepository<T> where T : Entity
{
    private readonly RestauranteDbContext _context;

    public BaseEntityRepository(RestauranteDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync(
        Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        bool includeAll = false)
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (includeAll)
        {
            query = IncludeAll(query);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        return await query.ToListAsync();
    }

    public async Task<PaginatedList<T>> GetPaginatedListAsync(
        int pageIndex, int pageSize, Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool includeAll = false)
    {
        IQueryable<T> query = _context.Set<T>();

        if (filter is not null)
        {
            query = query.Where(filter);
        }

        if (includeAll)
        {
            query = IncludeAll(query);
        }

        if (orderBy is not null)
        {
            query = orderBy(query);
        }

        var totalItems = await query.CountAsync();

        var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, totalItems, pageIndex, pageSize);
    }

    public IQueryable<T> GetQueryable()
    {
        return _context.Set<T>().AsQueryable();
    }

    public async Task<T?> GetByIdAsync(int id, bool includeAll = false)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includeAll)
        {
            query = IncludeAll(query);
        }

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T> AddAsync(T entity)
    {
        var entry = await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var entry = _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<T> DeleteAsync(int id)
    {
        try
        {
            var entity = await GetByIdAsync(id);
            if (entity is null)
            {
                throw new ArgumentException("Entidade nâo encontrada");
            }

            var entry = _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }
        catch (DbUpdateException e)
        {
            if (e.InnerException is not SqlException sqlException) throw;
            switch (sqlException.Number)
            {
                case 547:
                    throw new ConstraintException(
                        "Não é possível excluir o registro pois há conflito em outros registros");
                default:
                    throw;
            }
        }
    }

    private static IQueryable<T> IncludeAll(IQueryable<T> queryable)
    {
        var entityType = queryable.GetType().GetGenericArguments()[0];
        var properties = entityType.GetProperties()
            .Where(p => (p.PropertyType.IsClass && p.PropertyType != typeof(string)) || p.PropertyType.IsInterface &&
                p.PropertyType != typeof(IEnumerable<>));

        return properties.Aggregate(queryable, (current, property) => current.Include(property.Name));
    }
}