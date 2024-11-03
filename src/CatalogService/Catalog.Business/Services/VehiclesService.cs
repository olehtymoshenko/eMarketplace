using Catalog.Business.Services.Abstractions;
using Catalog.Persistence.Entities;
using Catalog.Persistence.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Business.Services;
public class VehiclesService(IVehicleRepository VehicleRepository) : BaseCrudService<Vehicle>(VehicleRepository), IVehiclesService
{

    // TODO Pagination
    public  async Task<IEnumerable<Vehicle>> GetVehiclesByClientAsync(int clientId)
    {
         return await VehicleRepository.Query().Where(x => x.ClientId == clientId).ToListAsync();
    }
}
