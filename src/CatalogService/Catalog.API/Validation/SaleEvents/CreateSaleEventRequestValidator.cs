using FluentValidation;

namespace Catalog.API.Contract.SaleEventsController.Requests;

public class CreateSaleEventRequestValidator : AbstractValidator<CreateSaleEventRequest>
{
    public CreateSaleEventRequestValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
        RuleFor(x => x.ClientId).GreaterThan(1);
        RuleFor(x => x.DateEventStart).GreaterThan(DateTime.UtcNow.AddMonths(-1));
        RuleFor(x => x.DateEventEnd).GreaterThan(DateTime.UtcNow.AddMonths(12));
    }
}
