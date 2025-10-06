using Catalog.Common.Enums;

namespace Catalog.API.Contract.VehiclesController.Responses;

public record VehicleDto(
    int Id, 
    string? Name, 
    string? Description, 
    decimal? Price, 
    string? Properties, 
    int Quantity, 
    VehicleCategory Category, 
    DateTime CreatedAt, 
    DateTime? UpdatedAt, 
    uint Version, 
    int ClientId);
