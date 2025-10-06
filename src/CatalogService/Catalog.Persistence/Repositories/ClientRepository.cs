using Catalog.Persistence.Entities;
using Catalog.Persistence.Repositories.Abstractions;

namespace Catalog.Persistence.Repositories;
public class ClientRepository : GenericRepository<Client>, IClientRepository
{
    public ClientRepository(CatalogDbContext dbContext) : base(dbContext)
    {
    }
}
