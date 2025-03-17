using FraudSysApi.Models.Enums;
using FraudSysApi.Models.TransactionModels;

namespace FraudSysApi.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionState> ValidateTransaction(ValidateTransaction transaction);
    }
}
