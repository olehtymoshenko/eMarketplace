using Catalog.Common.Models;
using Catalog.Common.Result;
using Catalog.Persistence.Entities;

namespace Catalog.Business.Services.Abstractions;
public interface ISaleEventsService : ICrudService<SaleEvent>
{
    Task<Result<SaleEvent?>> GetVehiclesInSaleEventAsync(int saleEventId, PaginationQuery paginationQuery);

    Task<Result<int>> AssignVehiclesToSaleEventAsync(int saleEventId, IEnumerable<SaleEventVehicle> saleEventVehicles);
}
