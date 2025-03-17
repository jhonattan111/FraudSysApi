using FraudSysApi.Models.Enums;
using FraudSysApi.Models.TransactionModels;
using FraudSysApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FraudSysApi.Controllers
{
    public class TransactionController(ITransactionService transactionService) : GenericController
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> ValidateTransaction([FromBody] ValidateTransaction transaction)
        {
            TransactionState state = await transactionService.ValidateTransaction(transaction);
            return Ok();
        }
    }
}
