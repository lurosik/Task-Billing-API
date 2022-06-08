using BillingApi.Model;
using FluentValidation;

namespace BillingApi.Validators
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.PayableAmount).GreaterThan(0).WithMessage("{PropertyName} is not a positive value");
        }
       
    }
}
