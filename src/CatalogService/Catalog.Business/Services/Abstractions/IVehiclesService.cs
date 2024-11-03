using Catalog.Persistence.Entities;

namespace Catalog.Business.Services.Abstractions;
public interface IVehiclesService : ICrudService<Vehicle>
{
    Task<IEnumerable<Vehicle>> GetVehiclesByClientAsync(int clientId);
}
