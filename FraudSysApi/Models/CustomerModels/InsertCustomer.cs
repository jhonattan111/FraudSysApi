using FluentValidation;
using FraudSysApi.Utils;

namespace FraudSysApi.Models.CustomerModels
{
    public record InsertCustomer(string Document, string AgencyNumber, string AccountNumber, decimal PixTransactionLimit);

    public class InsertCustomerValidator : AbstractValidator<InsertCustomer>
    {
        public InsertCustomerValidator()
        {
            RuleFor(x => x.Document)
            .NotEmpty().WithMessage("shouldNotBeEmpty")
            .Custom((document, context) =>
            {
                if (!document.IsValid())
                {
                    context.AddFailure("shouldHaveValidDocument");
                }
            });

            RuleFor(x => x.AgencyNumber).NotEmpty().Length(4).WithMessage("shouldHaveFourDigits");
            RuleFor(x => x.AccountNumber).NotEmpty().Length(6).WithMessage("shouldHaveSixDigits");
            RuleFor(x => x.PixTransactionLimit).GreaterThan(0).WithMessage("shouldHaveValueGreatherThanZero");
        }
    }
}
