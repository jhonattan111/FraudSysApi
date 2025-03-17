using FraudSysApi.Models;
using FraudSysApi.Models.CustomerModels;
using FraudSysApi.Models.Enums;
using FraudSysApi.Models.TransactionModels;
using FraudSysApi.Services.Interfaces;

namespace FraudSysApi.Services
{
    public class TransactionService(ICustomerService customerService) : ITransactionService
    {
        public async Task<ApiResponse<string>> ValidateTransaction(InsertTransaction transaction)
        {
            Customer fromCustomer = await customerService.GetModel(transaction.FromDocument);
            if(fromCustomer is null)
            {
                return ApiResponse<string>.Error(TransactionState.ACCOUNT_NOT_FOUND.ToString(), StatusCodes.Status404NotFound);
            }

            Customer toCustomer = await customerService.GetModel(transaction.ToDocument);
            if (toCustomer is null)
            {
                return ApiResponse<string>.Error(TransactionState.ACCOUNT_NOT_FOUND.ToString(), StatusCodes.Status404NotFound);
            }

            if (fromCustomer.PixTransactionLimit < transaction.Value)
            {
                return ApiResponse<string>.Error(TransactionState.INSUFFICIENT_FUNDS.ToString(), StatusCodes.Status403Forbidden);
            }

            return ApiResponse<string>.Success(TransactionState.VALID.ToString());
        }
    }
}
