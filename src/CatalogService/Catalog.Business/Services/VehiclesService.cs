using Catalog.Business.Models;
using Catalog.Business.Services.Abstractions;
using Catalog.Common.Result;
using Catalog.Common.Utils;
using Catalog.Persistence.Entities;
using Catalog.Persistence.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Business.Services;
public class VehiclesService(IVehicleRepository VehicleRepository) : BaseCrudService<Vehicle>(VehicleRepository), IVehiclesService
{

    // TODO Pagination
    public  async Task<Result<IEnumerable<Vehicle>>> GetVehiclesByClientAsync(int clientId)
    {
         return await VehicleRepository.Query().Where(x => x.ClientId == clientId).ToListAsync();
    }


    public async Task<Result<int>> BulkVehiclesUploadFromCsvAsync(MemoryStream csv, int clientId)
    {
        var vehiclesCsvModel = CsvReader.ReadFromCsv<VehicleCsvModel>(csv);

        if(vehiclesCsvModel.IsFailure)
        {
            return vehiclesCsvModel.Error;
        }

        if(vehiclesCsvModel.Value == null || vehiclesCsvModel.Value.Count == 0)
        {
            return Errors.RequestGenericError("Unable to extract any records from the provided .CSV file. Please ensure it's valid");
        }

        var newVehicles = vehiclesCsvModel.Value.Select(x => new Vehicle()
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
