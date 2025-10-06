namespace Catalog.API.Contract.ClientsController.Requests;

public record UpdateClientRequest(
    int Id,
    string? Name,
    string? LocationRegion,
    string? City,
    string? ZipCode,
    string? Address,
    string? Phone,
    uint Version) : CreateClientRequest(Name, LocationRegion, City, ZipCode, Address, Phone);
