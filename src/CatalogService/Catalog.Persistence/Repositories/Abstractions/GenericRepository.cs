using Catalog.Common.Result;
using Catalog.Persistence.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.Repositories.Abstractions;
public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    public CatalogDbContext DbContext { get; }
    protected DbSet<TEntity> Entities { get; }

    public GenericRepository(CatalogDbContext dbContext)
    {
        DbContext = dbContext;
        Entities = DbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(int pageSize, int pageNumber)
    {
        return await Entities
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Skip(pageSize * (pageNumber-1))
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        if(id < 0) throw new ArgumentException($"Argument {nameof(id)} is less than zero in {nameof(GenericRepository<TEntity>)}.{nameof(GetAllAsync)}");

        return await Entities.FindAsync(id);
    }

    public IQueryable<TEntity> Query()
    {
        return Entities.AsQueryable();
    }


    public async Task<int> AddAsync(TEntity entity, bool persistChanges = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        Entities.Add(entity);
        return persistChanges ? (await DbContext.SaveChangesAsync()) : 0;
    }

    public async Task<int> AddRangeAsync(IEnumerable<TEntity> entities, bool persistChanges = true)
    {
        if (entities == null || entities.Any(x => x == default)) throw new ArgumentNullException(nameof(entities));

        Entities.AddRange(entities);
        return persistChanges ? (await DbContext.SaveChangesAsync()) : 0;
    }

    public async Task<int> DeleteAsync(TEntity entity, bool persistChanges = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        Entities.Remove(entity);
        return persistChanges ? (await DbContext.SaveChangesAsync()) : 0;
    }

    public async Task<int> UpdateAsync(TEntity entity, bool persistChanges = true)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        Entities.Update(entity);
        var updatedRows = persistChanges ? (await DbContext.SaveChangesAsync()) : 0;

        // Please note - the only reason for this is to get the CreatedAt value, and one should think on how to get rid of the second db call
        await DbContext.Entry(entity).ReloadAsync(); 
        return updatedRows;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await DbContext.SaveChangesAsync();
    }
}
