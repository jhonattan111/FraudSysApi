﻿using FluentValidation;
using FraudSysApi.Utils;

namespace FraudSysApi.Models.CustomerModels
{
    public record InsertCustomer(string Document, string AgencyNumber, string AccountNumber, decimal PixTransactionLimit);

    public class InsertCustomerValidator : AbstractValidator<InsertCustomer>
    {
        public InsertCustomerValidator()
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
            //RuleFor(x => x.PixTransactionLimit).GreaterThan(0).WithMessage("SHOULD_HAVE_VALUE_GREATHER_THAN_ZERO");
        }
    }
}
