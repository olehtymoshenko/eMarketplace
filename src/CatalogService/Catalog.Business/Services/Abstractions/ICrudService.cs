using Catalog.Business.Models;
using Catalog.Persistence.Entities.Abstractions;

namespace Catalog.Business.Services.Abstractions;

public interface ICrudService<TEntity>
    where TEntity : BaseEntity
{
    public Task<IEnumerable<TEntity>> GetAllAsync(PaginationQuery paginationQuery);

    public Task<TEntity?> GetByIdAsync(int id);

    public Task<TEntity?> CreateAsync(TEntity entity);

    public Task<TEntity?> UpdateAsync(int id, TEntity entity);

    public Task<int> DeleteAsync(int id, TEntity entity);
}
