namespace Catalog.API.Contract.ClientsController.Requests;

public record CreateClientRequest(
    string Name,
    string? LocationRegion,
    string? City,
    string? ZipCode,
    string? Address,
    string Phone);
