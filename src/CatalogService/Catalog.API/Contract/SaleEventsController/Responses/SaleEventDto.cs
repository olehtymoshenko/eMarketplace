namespace Catalog.API.Contract.SaleEventsController.Responses;

public record SaleEventDto(
    int Id, 
    string Name, 
    DateTime DateEventStart, 
    DateTime DateEventEnd, 
    DateTime CreatedAt, 
    DateTime? UpdatedAt, 
    uint Version, 
    int ClientId);
