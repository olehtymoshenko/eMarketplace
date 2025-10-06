using FluentValidation;

namespace Catalog.API.Contract.SaleEventsController.Requests;


public class AssignVehiclesToSaleEventRequestValidator : AbstractValidator<AssignVehiclesToSaleEventRequest>
{
    public AssignVehiclesToSaleEventRequestValidator()
    {
        RuleFor(x => x.Vehicles).NotEmpty();
        
        RuleForEach(x => x.Vehicles).ChildRules(c =>
        {
            c.RuleFor(e => e.VehicleId).GreaterThan(0);
            c.RuleFor(e => e.DiscountPercent).InclusiveBetween(1, 100);
        });
    }
}