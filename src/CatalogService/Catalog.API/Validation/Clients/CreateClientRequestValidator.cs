using Catalog.API.Contract.ClientsController.Requests;
using FluentValidation;

namespace Catalog.API.Validation.Clients;

public class CreateClientRequestValidator : AbstractValidator<CreateClientRequest>
{
    public CreateClientRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Phone).NotEmpty().Matches(@"^(\+\d{2})(\d{3,10})?$");
        RuleFor(x => x.City).NotEmpty().MaximumLength(75).When(x => !string.IsNullOrWhiteSpace(x.City));
        RuleFor(x => x.Address).NotEmpty().MaximumLength(125).When(x => !string.IsNullOrWhiteSpace(x.Address));
        RuleFor(x => x.LocationRegion).NotEmpty().MaximumLength(125).When(x => !string.IsNullOrWhiteSpace(x.LocationRegion));
        RuleFor(x => x.ZipCode).NotEmpty().Matches(@"^\d{5}(?:[-\s]\d{4})?$").When(x => !string.IsNullOrWhiteSpace(x.ZipCode));
    }
}
