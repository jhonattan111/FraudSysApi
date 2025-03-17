using FraudSysApi.Models;
using FraudSysApi.Models.TransactionModels;

namespace FraudSysApi.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<ApiResponse<string>> ValidateTransaction(InsertTransaction transaction);
    }
}
