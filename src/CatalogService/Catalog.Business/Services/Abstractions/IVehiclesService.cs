using Catalog.Common.Result;
using Catalog.Persistence.Entities;

namespace Catalog.Business.Services.Abstractions;
public interface IVehiclesService : ICrudService<Vehicle>
{
    Task<Result<IEnumerable<Vehicle>>> GetVehiclesByClientAsync(int clientId);

    Task<Result<int>> BulkVehiclesUploadFromCsvAsync(MemoryStream file, int clientId);
}
