using Catalog.Business.Models;
using Catalog.Business.Services.Abstractions;
using Catalog.Persistence.Entities;
using Catalog.Persistence.Repositories;
using Catalog.Persistence.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Catalog.Business.Services;
public class SaleEventsService(ISaleEventRepository SaleEventsRepository) : BaseCrudService<SaleEvent>(SaleEventsRepository), ISaleEventsService
{
    /// <summary>
    /// Allows to assign multiple vehicles to one SaleEvent
    /// </summary>
    public async Task<int> AssignVehiclesToSaleEventAsync(int saleEventId, IEnumerable<SaleEventVehicle> saleEventVehicles)
    {
        if (saleEventVehicles == null || !saleEventVehicles.Any())
        {
            return 0;
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

    public async Task<SaleEvent> GetVehiclesInSaleEventAsync(int saleEventId, PaginationQuery paginationQuery)
    {
        return await SaleEventsRepository.Query()
            .AsNoTracking()
            .Where(x => x.Id == saleEventId)
            .Include(x => x.Vehicles
                            .OrderBy(x => x.Id)
                            .Skip(paginationQuery.PageSize * (paginationQuery.PageNumber - 1))
                            .Take(paginationQuery.PageSize))
            .FirstAsync();
    }
}
