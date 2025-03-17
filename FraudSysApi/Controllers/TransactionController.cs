using FraudSysApi.Models.TransactionModels;
using FraudSysApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FraudSysApi.Controllers
{
    public class TransactionController(ITransactionService transactionService) : GenericController
    {
        [HttpPost("[action]")]
        public Task<IActionResult> ValidateTransaction([FromBody] ValidateTransaction transaction)
        {
            transactionService.ValidateTransaction();

            return Ok();
        }
    }
}
