using Catalog.API.Contract.VehiclesController.Requests;
using FluentValidation;

namespace Catalog.API.Validation.Vehicles;

public class DeleteVehicleRequestValidator : AbstractValidator<DeleteVehicleRequest>
{
    public DeleteVehicleRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Version).NotEmpty();
    }
}
