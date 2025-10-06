using AutoMapper;
using Catalog.API.Contract.ClientsController.Requests;
using Catalog.API.Contract.ClientsController.Responses;
using Catalog.API.Contract.SaleEventsController.Requests;
using Catalog.API.Contract.SaleEventsController.Responses;
using Catalog.API.Contract.VehiclesController.Requests;
using Catalog.API.Contract.VehiclesController.Responses;
using Catalog.Persistence.Entities;

namespace Catalog.API.Mapping;

public class PresentationLayerProfile : Profile
{
    public PresentationLayerProfile()
    {
        // Clients
        CreateMap<CreateClientRequest, Client>();
        CreateMap<UpdateClientRequest, Client>();
        CreateMap<DeleteClientRequest, Client>();
        CreateMap<Client, ClientDto>();

        // Vehicles
        CreateMap<CreateVehicleRequest, Vehicle>();
        CreateMap<UpdateVehicleRequest, Vehicle>();
        CreateMap<DeleteVehicleRequest, Vehicle>();
        CreateMap<Vehicle, VehicleDto>();

        // SaleEvents
        CreateMap<CreateSaleEventRequest, SaleEvent>();
        CreateMap<UpdateSaleEventRequest, SaleEvent>();
        CreateMap<DeleteSaleEventRequest, SaleEvent>();
        CreateMap<SaleEvent, SaleEventDto>();
        CreateMap<SaleEvent, GetVehiclesInSaleEventResponse>();

    }
}
