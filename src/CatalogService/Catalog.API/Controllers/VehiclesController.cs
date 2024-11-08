using AutoMapper;
using Catalog.API.Contract.VehiclesController.Requests;
using Catalog.API.Contract.VehiclesController.Responses;
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
    public async Task<IActionResult> GetProductsByClient(int id)
    {
        var vehicles = await _vehiclesService.GetVehiclesByClientAsync(id);

        return vehicles != null ? Ok(Mapper.Map<List<VehicleDto>>(vehicles)) : Ok();
    }


    [HttpPost("client/{clientId:int}/bulk-upload-csv")]
    public async Task<IActionResult> BulkVehiclesUploadFromCsv(int clientId, IFormFile vehiclesAsFile)
    {
        using var ms = new MemoryStream();
        using var file = vehiclesAsFile.OpenReadStream();
        file.CopyTo(ms);
        ms.Position = 0;

        var uploadedVehiclesNumber = await _vehiclesService.BulkVehiclesUploadFromCsvAsync(ms, clientId);

        return uploadedVehiclesNumber > 0 ? Ok(uploadedVehiclesNumber) : StatusCode(500);
    }
}
