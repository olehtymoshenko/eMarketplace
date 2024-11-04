namespace Catalog.API.Contract.SaleEventsController.Requests;

public record AssignVehiclesToSaleEventRequest(
    IEnumerable<VehicleSaleEvent> Vehicles);


public record VehicleSaleEvent(
    int VehicleId,
    decimal DiscountPercent);
