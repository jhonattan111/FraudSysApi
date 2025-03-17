using FraudSysApi.Models.TransactionModels;
using FraudSysApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FraudSysApi.Controllers
{
    public class TransactionController(ITransactionService transactionService) : GenericController
    {
        [HttpPost("[action]")]
        public async Task<IActionResult> ValidateTransaction([FromBody] InsertTransaction transaction)
        {
            try
            {
                Models.ApiResponse<string> response = await transactionService.ValidateTransaction(transaction);
                return StatusCode(response.StatusCode, new { response.Message, response.Data });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
