using Catalog.Business.Services.Abstractions;
using Catalog.Common.Models;
using Catalog.Common.Result;
using Catalog.Persistence.Entities;
using Catalog.Persistence.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Business.Services;
public class SaleEventsService(ISaleEventRepository SaleEventsRepository) : BaseCrudService<SaleEvent>(SaleEventsRepository), ISaleEventsService
{
    /// <summary>
    /// Allows to assign multiple vehicles to one SaleEvent
    /// </summary>
    public async Task<Result<int>> AssignVehiclesToSaleEventAsync(int saleEventId, IEnumerable<SaleEventVehicle> saleEventVehicles)
    {
        if (saleEventVehicles == null || !saleEventVehicles.Any())
        {
            return Errors.RequestGenericError("List of provided vehicles is null or empty, or cannot be parsed");
        }

        var vehicleIds = saleEventVehicles.Select(x => x.VehicleId).ToList();

        var existingRecords = await SaleEventsRepository.DbContext.SaleEventVehicles
            .Where(x => x.SaleEventId == saleEventId && vehicleIds.Contains(x.VehicleId))
            .ToListAsync();

        foreach(var existingRecord in existingRecords)
        {
            existingRecord.Discount = saleEventVehicles
                .FirstOrDefault(x => x.VehicleId == existingRecord.VehicleId && x.SaleEventId == existingRecord.SaleEventId)
                ?.Discount ?? existingRecord.Discount;
        }

        var newSaleVehicleRecords = saleEventVehicles.Where(x => !existingRecords.Any(e => e.VehicleId == x.VehicleId && e.SaleEventId == x.SaleEventId));
        SaleEventsRepository.DbContext.SaleEventVehicles.AddRange(newSaleVehicleRecords);
        
        return await SaleEventsRepository.SaveChangesAsync();
    }

    public async Task<Result<SaleEvent?>> GetVehiclesInSaleEventAsync(int saleEventId, PaginationQuery paginationQuery)
    {
        return await SaleEventsRepository.Query()
            .AsNoTracking()
            .Where(x => x.Id == saleEventId)
            .Include(x => x.Vehicles
                            .OrderBy(x => x.Id)
                            .Skip(paginationQuery.PageSize * (paginationQuery.PageNumber - 1))
                            .Take(paginationQuery.PageSize))
            .FirstOrDefaultAsync();
    }
}
