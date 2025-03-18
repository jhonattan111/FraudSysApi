using FraudSysApi.Models;
using FraudSysApi.Models.CustomerModels;
using FraudSysApi.Models.TransactionModels;
using FraudSysApi.Repositories.Interfaces;
using FraudSysApi.Services.Interfaces;

namespace FraudSysApi.Services
{
    public class TransactionService(ICustomerService customerService, ITransactionRepository transactionRepository) : ITransactionService
    {
        public async Task<ApiResponse<string>> ValidateTransaction(InsertTransaction transaction)
        {
            if(transaction.FromDocument == transaction.ToDocument)
            {
                return ApiResponse<string>.Error("SAME_ACCOUNT", StatusCodes.Status406NotAcceptable);
            }

            Customer fromCustomer = await customerService.GetModel(transaction.FromDocument);
            if (fromCustomer is null)
            {
                return ApiResponse<string>.Error("ACCOUNT_FROM_NOT_FOUND", StatusCodes.Status404NotFound);
            }

            Customer toCustomer = await customerService.GetModel(transaction.ToDocument);
            if (toCustomer is null)
            {
                return ApiResponse<string>.Error("ACCOUNT_TO_NOT_FOUND", StatusCodes.Status404NotFound);
            }

            if (fromCustomer.PixTransactionLimit < transaction.Value)
            {
                return ApiResponse<string>.Error("INSUFFICIENT_FUNDS", StatusCodes.Status403Forbidden);
            }

            decimal newFromLimit = fromCustomer.PixTransactionLimit - transaction.Value;

            await customerService.UpdatePixTransactionLimit(transaction.FromDocument, newFromLimit);

            return ApiResponse<string>.Success("VALID");
        }
    }
}
