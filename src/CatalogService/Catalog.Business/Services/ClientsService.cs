using Catalog.Business.Services.Abstractions;
using Catalog.Persistence.Entities;
using Catalog.Persistence.Repositories.Abstractions;

namespace Catalog.Business.Services;
public class ClientsService(IClientRepository ClientRepository) : BaseCrudService<Client>(ClientRepository), IClientsService 
{ 
    
}
