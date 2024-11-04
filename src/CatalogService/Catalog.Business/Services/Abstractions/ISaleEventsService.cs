using Catalog.Business.Models;
using Catalog.Persistence.Entities;

namespace Catalog.Business.Services.Abstractions;
public interface ISaleEventsService : ICrudService<SaleEvent>
{
    Task<SaleEvent> GetVehiclesInSaleEventAsync(int saleEventId, PaginationQuery paginationQuery);

    Task<int> AssignVehiclesToSaleEventAsync(int saleEventId, IEnumerable<SaleEventVehicle> saleEventVehicles);
}
