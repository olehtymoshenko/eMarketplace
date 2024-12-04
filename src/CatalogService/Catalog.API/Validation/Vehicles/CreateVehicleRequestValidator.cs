using Catalog.API.Contract.VehiclesController.Requests;
using FluentValidation;

namespace Catalog.API.Validation.Vehicles;

public class CreateVehicleRequestValidator : AbstractValidator<CreateVehicleRequest>
{
    public CreateVehicleRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Price).GreaterThan(0).When(x => x.Price != null);
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).When(x => x.Quantity != null);
        RuleFor(x => x.Category).IsInEnum();
        RuleFor(x => x.ClientId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Description).MaximumLength(1024).When(x => !string.IsNullOrWhiteSpace(x.Description));
        RuleFor(x => x.Properties).MaximumLength(1024).When(x => !string.IsNullOrWhiteSpace(x.Description));
    }
}
