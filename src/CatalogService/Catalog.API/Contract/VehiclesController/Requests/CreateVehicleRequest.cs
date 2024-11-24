using Catalog.Common.Enums;

namespace Catalog.API.Contract.VehiclesController.Requests;

public record CreateVehicleRequest(
    string? Name, 
    string? Description, 
    decimal? Price, 
    string? Properties, 
    int Quantity, 
    VehicleCategory Category, 
    int ClientId);
