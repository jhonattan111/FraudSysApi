using FraudSysApi.Models.Enums;
using FraudSysApi.Models.TransactionModels;
using FraudSysApi.Services.Interfaces;

namespace FraudSysApi.Services
{
    public class TransactionService : ITransactionService
    {
        public async Task<TransactionState> ValidateTransaction(ValidateTransaction transaction)
        {
            return TransactionState.VALID;
        }
    }
}
