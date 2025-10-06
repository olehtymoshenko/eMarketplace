using Catalog.Common.Models;
using Catalog.Common.Result;
using Catalog.Persistence.Entities.Abstractions;
using Catalog.Persistence.Repositories.Abstractions;

namespace Catalog.Business.Services.Abstractions;
public class BaseCrudService<TEntity> : ICrudService<TEntity>
    where TEntity : BaseEntity
{
    protected IGenericRepository<TEntity> GenericRepository { get; }
    
    public BaseCrudService(IGenericRepository<TEntity> genericRepository)
    {
        GenericRepository = genericRepository;
    }

    public async Task<Result<IEnumerable<TEntity>>> GetAllAsync(PaginationQuery paginationQuery)
    {
        var resourceCollection = await GenericRepository.GetAllAsync(paginationQuery.PageSize, paginationQuery.PageNumber);

        return Result<IEnumerable<TEntity>>.Success(resourceCollection);
    }

    public async Task<Result<TEntity?>> GetByIdAsync(int id)
    {
        var resource = await GenericRepository.GetByIdAsync(id);

        return resource;
    }

    public async Task<Result<TEntity?>> CreateAsync(TEntity entity)
    {
        if (entity == null)
        {
            return Errors.RequestGenericError("The object model in the request is null or empty");
        }

        return (await GenericRepository.AddAsync(entity)) > 0 ? entity : Errors.RequestGenericError("Failed to create a new entity");
    }

    public async Task<Result<TEntity?>> UpdateAsync(int id, TEntity entity)
    {
        if (id <= 0 || entity == null || entity.Id != id)
        {
            return Errors.RequestGenericError("The object id in the body is less than 1, or object is null/empty, or object id in the request body doesn't match id in the URL");
        }

        return (await GenericRepository.UpdateAsync(entity)) > 0 ? entity : Errors.ServerInternalError;
    }

    public async Task<Result<int>> DeleteAsync(int id, TEntity entity)
    {
        if (entity == null || entity.Id <= 0 || entity.Id != id)
        {
            return Errors.RequestGenericError("The object id is less than 0, or object is null/empty or object id in the request body doesn't match id in the URL");
        }

        var recordsDeleted = await GenericRepository.DeleteAsync(entity);

        return recordsDeleted > 0 ? recordsDeleted : Errors.ServerInternalError;
    }
}
