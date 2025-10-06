namespace Catalog.API.Contract.ClientsController.Responses;

public record ClientDto(
    int Id,
    string? Name,
    string? LocationRegion,
    string? City,
    string? ZipCode,
    string? Address,
    string? Phone,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    uint Version);
