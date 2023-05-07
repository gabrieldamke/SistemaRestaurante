using System.Linq.Expressions;
using Domain.Entities.Shared;
using Domain.Types;

namespace Domain.Interfaces;

public interface IEntityRepository<TEntity> where TEntity : Entity
{
    Task<PaginatedList<TEntity>> GetPaginatedListAsync(
        int pageIndex, int pageSize, Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool includeAll = false);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool includeAll = false);

    IQueryable<TEntity> GetQueryable();
    Task<TEntity?> GetByIdAsync(int id, bool includeAll = false);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(int id);
}