using FluentValidation;
using FraudSysApi.Utils;

namespace FraudSysApi.Models.CustomerModels
{

    public record UpdateCustomer(string Document, string AgencyNumber, string AccountNumber);

    public class UpdateCustomerValidator : AbstractValidator<UpdateCustomer>
    {
        public UpdateCustomerValidator()
        {
            RuleFor(x => x.Document)
            .NotEmpty().WithMessage("SHOULD_NOT_BE_EMPTY")
            .Custom((document, context) =>
            {
                if (!document.IsValid())
                {
                    context.AddFailure("SHOULD_HAVE_VALID_DOCUMENT");
                }
            });

            RuleFor(x => x.AgencyNumber).NotEmpty().Length(4).WithMessage("SHOULD_HAVE_FOUR_DIGITS");
            RuleFor(x => x.AccountNumber).NotEmpty().Length(6).WithMessage("SHOULD_HAVE_SIX_DIGITS");
        }
    }
}
