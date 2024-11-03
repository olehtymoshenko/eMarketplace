using AutoMapper;
using Catalog.API.Contract.SaleEventsController.Requests;
using Catalog.API.Contract.SaleEventsController.Responses;
using Catalog.Business.Services.Abstractions;
using Catalog.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class SaleEventsController : BaseCrudController<SaleEvent, SaleEventDto, CreateSaleEventRequest, UpdateSaleEventRequest, DeleteSaleEventRequest>
{
    public ISaleEventsService SaleEventsService { get; }
    public ILogger<ClientsController> Logger { get; }


    public SaleEventsController(ISaleEventsService saleEventsService, IMapper mapper, ILogger<ClientsController> logger) : base(saleEventsService, mapper)
    {
        SaleEventsService = saleEventsService;
        Logger = logger;
    }
}