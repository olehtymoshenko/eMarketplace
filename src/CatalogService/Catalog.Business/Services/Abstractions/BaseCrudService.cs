using Catalog.Business.Models;
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

    public async Task<IEnumerable<TEntity>> GetAllAsync(PaginationQuery paginationQuery)
    {
        var resourceCollection = await GenericRepository.GetAllAsync(paginationQuery.PageSize, paginationQuery.PageNumber);
        return resourceCollection ?? Array.Empty<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var resource = await GenericRepository.GetByIdAsync(id);
        return resource ?? null;
    }

    public async Task<TEntity?> CreateAsync(TEntity entity)
    {
        if (entity == null)
        {
            return null;
        }

        return (await GenericRepository.AddAsync(entity)) > 0 ? entity : null;
    }

    public async Task<TEntity?> UpdateAsync(int id, TEntity entity)
    {
        if (id <= 0 || entity == null || entity.Id != id)
        {
            return null;
        }
        
        return (await GenericRepository.UpdateAsync(entity)) > 0 ? entity : null;
    }

    public async Task<int> DeleteAsync(int id, TEntity entity)
    {
        if (entity == null || entity.Id <= 0 || entity.Id != id)
        {
            return 0;
        }

       return await GenericRepository.DeleteAsync(entity);
    }
}
