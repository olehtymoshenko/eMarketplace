using AutoMapper;
using Catalog.API.Contract.SaleEventsController.Requests;
using Catalog.API.Contract.SaleEventsController.Responses;
using Catalog.Business.Models;
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

    [HttpGet("{saleEventId:int}/vehicles")]
    public async Task<IActionResult> GetVehiclesInSaleEvent(int saleEventId, [FromQuery]PaginationQuery paginationQuery)
    {
        var saleEventWithVehicles = await SaleEventsService.GetVehiclesInSaleEventAsync(saleEventId, paginationQuery ?? new());

        var result = Mapper.Map<GetVehiclesInSaleEventResponse>(saleEventWithVehicles);

        return result != null ? Ok(result) : BadRequest();
    }

    [HttpPost("{saleEventId:int}/vehicles")]
    public async Task<IActionResult> AssignVehiclesToSaleEvent(int saleEventId, AssignVehiclesToSaleEventRequest request)
    {
        var vehiclesToBeAssignedToSaleEvent=  request.Vehicles.Select(x => new SaleEventVehicle()
        {
            Discount = x.DiscountPercent,
            VehicleId = x.VehicleId,
            SaleEventId = saleEventId
        });

        var affectedRows = await SaleEventsService.AssignVehiclesToSaleEventAsync(saleEventId, vehiclesToBeAssignedToSaleEvent);

        return Ok(affectedRows);
    }
}