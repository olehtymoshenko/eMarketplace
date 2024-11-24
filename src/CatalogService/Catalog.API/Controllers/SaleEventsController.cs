using AutoMapper;
using Catalog.API.Contract.SaleEventsController.Requests;
using Catalog.API.Contract.SaleEventsController.Responses;
using Catalog.API.Mapping;
using Catalog.Business.Services.Abstractions;
using Catalog.Common.Models;
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
    public async Task<IResult> GetVehiclesInSaleEvent(int saleEventId, [FromQuery]PaginationQuery paginationQuery)
    {
        var saleEventWithVehiclesResult = await SaleEventsService.GetVehiclesInSaleEventAsync(saleEventId, paginationQuery ?? new());

        return saleEventWithVehiclesResult.Match(
            value => value != default ? Results.Ok(Mapper.Map<GetVehiclesInSaleEventResponse>(value)) : Results.Ok(),
            err => err.MapToResponse());
    }

    [HttpPost("{saleEventId:int}/vehicles")]
    public async Task<IResult> AssignVehiclesToSaleEvent(int saleEventId, AssignVehiclesToSaleEventRequest request)
    {
        var vehiclesToBeAssignedToSaleEvent = request.Vehicles.Select(x => new SaleEventVehicle()
        {
            Discount = x.DiscountPercent,
            VehicleId = x.VehicleId,
            SaleEventId = saleEventId
        });

        var affectedRowsResult = await SaleEventsService.AssignVehiclesToSaleEventAsync(saleEventId, vehiclesToBeAssignedToSaleEvent);

        return affectedRowsResult.Match(
            value => Results.Ok(affectedRowsResult),
            err => err.MapToResponse());
    }
}