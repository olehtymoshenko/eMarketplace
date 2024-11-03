using Catalog.Business.Services;
using Catalog.Business.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Catalog.Business.Extensions;


public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services)
    {
        services.TryAddScoped<IVehiclesService, VehiclesService>();
        services.TryAddScoped<IClientsService, ClientsService>();
        services.TryAddScoped<ISaleEventsService, SaleEventsService>();

        return services;
    }
}
