using AutoMapper;
using Catalog.API.Contract.VehiclesController.Requests;
using Catalog.API.Contract.VehiclesController.Responses;
using Catalog.API.Mapping;
using Catalog.Business.Services.Abstractions;
using Catalog.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VehiclesController : BaseCrudController<Vehicle, VehicleDto, CreateVehicleRequest, UpdateVehicleRequest, DeleteVehicleRequest>
{
    private readonly IVehiclesService _vehiclesService;

    public VehiclesController(IVehiclesService vehiclesService, IMapper mapper) : base(vehiclesService, mapper)
    {
        _vehiclesService = vehiclesService;
    }


    [HttpGet("client/{id:int}")]
    public async Task<IResult> GetProductsByClient(int id)
    {
        var vehiclesResult = await _vehiclesService.GetVehiclesByClientAsync(id);

        return vehiclesResult.Match(
            value => value != default ? Results.Ok(Mapper.Map<List<VehicleDto>>(vehiclesResult)) : Results.Ok(),
            err => err.MapToResponse());
    }


    [HttpPost("client/{clientId:int}/bulk-upload-csv")]
    public async Task<IResult> BulkVehiclesUploadFromCsv(int clientId, IFormFile vehiclesAsFile)
    {
        if (clientId < 0) throw new Exception("Damn, invalid body");
        using var ms = new MemoryStream();
        using (var file = vehiclesAsFile.OpenReadStream())
        {
            file.CopyTo(ms);
        }
        ms.Position = 0;
        
        var uploadedVehiclesNumber = await _vehiclesService.BulkVehiclesUploadFromCsvAsync(ms, clientId);

        return uploadedVehiclesNumber.Match(
            value => value > 0 ? Results.Ok(value) : Results.BadRequest("No records have been added. Please ensure the request is correct."),
            err => err.MapToResponse());
    }
}
