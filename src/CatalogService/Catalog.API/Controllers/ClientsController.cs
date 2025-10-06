using AutoMapper;
using Catalog.API.Contract.ClientsController.Requests;
using Catalog.API.Contract.ClientsController.Responses;
using Catalog.Business.Services.Abstractions;
using Catalog.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : BaseCrudController<Client, ClientDto, CreateClientRequest, UpdateClientRequest, DeleteClientRequest>
{
    public IClientsService ClientsService { get; }
    public ILogger<ClientsController> Logger { get; }


    public ClientsController(IClientsService clientsService, IMapper mapper, ILogger<ClientsController> logger) : base(clientsService, mapper)
    {
        ClientsService = clientsService;
        Logger = logger;
    }
}
