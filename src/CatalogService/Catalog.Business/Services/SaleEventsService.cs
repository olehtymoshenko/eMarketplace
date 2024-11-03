using Catalog.Business.Services.Abstractions;
using Catalog.Persistence.Entities;
using Catalog.Persistence.Repositories.Abstractions;

namespace Catalog.Business.Services;
public class SaleEventsService(ISaleEventRepository SaleEventRepository) : BaseCrudService<SaleEvent>(SaleEventRepository), ISaleEventsService
{
}
