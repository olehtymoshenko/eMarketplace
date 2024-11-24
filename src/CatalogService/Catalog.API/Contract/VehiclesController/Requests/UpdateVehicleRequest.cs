using Catalog.Common.Enums;

namespace Catalog.API.Contract.VehiclesController.Requests;

public record UpdateVehicleRequest(
    int Id, 
    string? Name, 
    string? Description, 
    decimal? Price, 
    string? Properties, 
    int Quantity, 
    VehicleCategory Category, 
    uint Version, 
    int ClientId);
