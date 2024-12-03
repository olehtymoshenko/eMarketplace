using Catalog.API.Contract.VehiclesController.Requests;
using FluentValidation;

namespace Catalog.API.Validation.Vehicles;

public class CreateVehicleRequestValidator : AbstractValidator<CreateVehicleRequest>
{
    public CreateVehicleRequestValidator()
    {

    }
}
