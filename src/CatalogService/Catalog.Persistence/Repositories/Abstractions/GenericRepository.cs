using Catalog.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.Repositories.Abstractions;
public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{

    protected CatalogDbContext DbContext { get; }
    protected DbSet<TEntity> Entities { get; }

    public GenericRepository(CatalogDbContext dbContext)
    {
        DbContext = dbContext;
        Entities = DbContext.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await Entities.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        if(id < 0) throw new ArgumentNullException(nameof(id));

        return await Entities.FindAsync(id);
    }

    public IQueryable<TEntity> Query()
    {
        return Entities.AsQueryable();
    }


    public void Add(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        Entities.Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        if (entities == null || entities.Any(x => x == default)) throw new ArgumentNullException(nameof(entities));

        Entities.AddRange(entities);
    }

    public void Delete(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        Entities.Remove(entity);
    }
    public void Update(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        Entities.Update(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await DbContext.SaveChangesAsync();
    }
}
