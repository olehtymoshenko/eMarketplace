namespace Catalog.API.Contract.SaleEventsController.Requests;

public record UpdateSaleEventRequest(
    int Id,
    string Name,
    DateTime DateEventStart,
    DateTime DateEventEnd,
    int ClientId,
    uint Version) : CreateSaleEventRequest(Name, DateEventStart, DateEventEnd, ClientId);
