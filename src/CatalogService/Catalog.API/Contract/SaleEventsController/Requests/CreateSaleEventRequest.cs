namespace Catalog.API.Contract.SaleEventsController.Requests;

public record CreateSaleEventRequest(
    string Name, 
    DateTime DateEventStart, 
    DateTime DateEventEnd, 
    int ClientId);
