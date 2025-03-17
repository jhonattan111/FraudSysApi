using FluentValidation;
using FraudSysApi.Utils;

namespace FraudSysApi.Models.TransactionModels
{
    public record InsertTransaction(string FromDocument, string ToDocument, decimal Value);

    public class InsertTransactionValidator : AbstractValidator<InsertTransaction>
    {
        public InsertTransactionValidator()
        {
            RuleFor(x => x.FromDocument)
            .NotEmpty().WithMessage("shouldNotBeEmpty")
            .Custom((document, context) =>
            {
                if (!document.IsValid())
                {
                    context.AddFailure("shouldHaveValidDocument");
                }
            });

            RuleFor(x => x.ToDocument)
            .NotEmpty().WithMessage("shouldNotBeEmpty")
            .Custom((document, context) =>
            {
                if (!document.IsValid())
                {
                    context.AddFailure("shouldHaveValidDocument");
                }
            });

            RuleFor(x => x.Value).GreaterThan(0).WithMessage("shouldHaveValueGreatherThanZero");

        }
    }
}
