using FluentValidation;

namespace Catalog.API.Contract.SaleEventsController.Requests;

public class DeleteSaleEventRequestValidator : AbstractValidator<DeleteSaleEventRequest>
{
    public DeleteSaleEventRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Version).NotEmpty();
    }
}