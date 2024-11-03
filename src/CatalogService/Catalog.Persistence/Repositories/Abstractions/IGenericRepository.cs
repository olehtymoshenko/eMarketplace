using Catalog.Persistence.Entities.Abstractions;

namespace Catalog.Persistence.Repositories.Abstractions;
public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<int> SaveChangesAsync();

    Task<IEnumerable<TEntity>> GetAllAsync(int pageSize, int pageNumber);

    Task<TEntity?> GetByIdAsync(int id);

    Task<int> AddAsync(TEntity entity, bool persistChanges = true);

    Task<int> AddRangeAsync(IEnumerable<TEntity> entities, bool persistChanges = true);

    Task<int> UpdateAsync(TEntity entity, bool persistChanges = true);

    Task<int> DeleteAsync(TEntity entity, bool persistChanges = true);

    IQueryable<TEntity> Query();
}
