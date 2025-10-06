using FluentValidation;

namespace Catalog.API.Contract.SaleEventsController.Requests;

public class UpdateSaleEventRequestValidator : AbstractValidator<UpdateSaleEventRequest>
{
    public UpdateSaleEventRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Version).NotEmpty();

        RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
        RuleFor(x => x.ClientId).GreaterThan(1);
        RuleFor(x => x.DateEventStart).GreaterThan(DateTime.UtcNow.AddMonths(-1));
        RuleFor(x => x.DateEventEnd).GreaterThan(DateTime.UtcNow.AddMonths(12));
    }
}
