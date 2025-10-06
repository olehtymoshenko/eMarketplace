using Catalog.API.Contract.VehiclesController.Responses;

namespace Catalog.API.Contract.SaleEventsController.Responses;

public record GetVehiclesInSaleEventResponse(
    int Id,
    string Name,
    DateTime DateEventStart,
    DateTime DateEventEnd,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    uint Version,
    int ClientId)
{
    public IEnumerable<VehicleDto> Vehicles { get; init; } = [];
}
