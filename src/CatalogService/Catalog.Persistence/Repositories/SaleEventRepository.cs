using Catalog.Persistence.Entities;
using Catalog.Persistence.Repositories.Abstractions;

namespace Catalog.Persistence.Repositories;
public class SaleEventRepository(CatalogDbContext dbContext) : GenericRepository<SaleEvent>(dbContext), ISaleEventRepository
{

}
