namespace Catalog.API.Contract.VehiclesController.Requests;

public record DeleteVehicleRequest(
    int Id, 
    uint Version);
