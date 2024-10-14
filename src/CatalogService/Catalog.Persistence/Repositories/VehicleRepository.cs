using Catalog.Persistence.Entities;
using Catalog.Persistence.Repositories.Abstractions;

namespace Catalog.Persistence.Repositories;
public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
{
    public VehicleRepository(CatalogDbContext dbContext) : base(dbContext)
    {
    }
}
