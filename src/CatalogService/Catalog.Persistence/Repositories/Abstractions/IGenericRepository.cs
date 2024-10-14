using Catalog.Persistence.Entities;

namespace Catalog.Persistence.Repositories.Abstractions;
public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<int> SaveChangesAsync();

    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity?> GetByIdAsync(int id);

    void Add(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    IQueryable<TEntity> Query();
}
