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
            .NotEmpty().WithMessage("SHOULD_NOT_BE_EMPTY")
            .Custom((document, context) =>
            {
                if (!document.IsValid())
                {
                    context.AddFailure("SHOULD_HAVE_VALID_DOCUMENT");
                }
            });

            RuleFor(x => x.ToDocument)
            .NotEmpty().WithMessage("SHOULD_NOT_BE_EMPTY")
            .Custom((document, context) =>
            {
                if (!document.IsValid())
                {
                    context.AddFailure("SHOULD_HAVE_VALID_DOCUMENT");
                }
            });

            RuleFor(x => x.Value).GreaterThan(0).WithMessage("SHOULD_HAVE_VALUE_GREATHER_THAN_ZERO");

        }
    }
}
