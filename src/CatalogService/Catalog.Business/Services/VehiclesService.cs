using Catalog.Business.Models;
using Catalog.Business.Services.Abstractions;
using Catalog.Business.Utils;
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


    public async Task<int> BulkVehiclesUploadFromCsvAsync(MemoryStream csv, int clientId)
    {
        var vehiclesCsvModel = CsvReader.ReadFromCsv<VehicleCsvModel>(csv);

        var newVehicles = vehiclesCsvModel.Select(x => new Vehicle()
        {
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            Quantity = x.Quantity,
            Properties = x.Properties,
            Category = x.Category,
            ClientId = clientId
        });

        var insertedRows = await VehicleRepository.AddRangeAsync(newVehicles, true);

        return insertedRows;
    }
}
