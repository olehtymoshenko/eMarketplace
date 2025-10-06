using Catalog.Common.Models;
using Catalog.Common.Result;
using Catalog.Persistence.Entities.Abstractions;

namespace Catalog.Business.Services.Abstractions;

public interface ICrudService<TEntity>
    where TEntity : BaseEntity
{
    public Task<Result<IEnumerable<TEntity>>> GetAllAsync(PaginationQuery paginationQuery);

    public Task<Result<TEntity?>> GetByIdAsync(int id);

    public Task<Result<TEntity?>> CreateAsync(TEntity entity);

    public Task<Result<TEntity?>> UpdateAsync(int id, TEntity entity);

    public Task<Result<int>> DeleteAsync(int id, TEntity entity);
}
