using Customers.Api.Core;
using FluentValidation;

namespace Customers.Api.Features.CreateCustomer
{
    public class CreateCustomerRequestValidator : ModelValidatorBase<CreateCustomerRequest>
    {
        public CreateCustomerRequestValidator()
        {
            RuleFor(x => x.CorrelationId).NotNull().NotEmpty().WithMessage("correlation id cannot be null or empty");
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("title cannot be null or empty");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("first name cannot be null or empty");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("last name cannot be null or empty");
        }
    }
}