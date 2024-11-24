using Catalog.API.Contract.ClientsController.Requests;
using FluentValidation;

namespace Catalog.API.Validation.Clients;

public class DeleteClientRequestValidator : AbstractValidator<DeleteClientRequest>
{
    public DeleteClientRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Version).NotEmpty();
    }
}
