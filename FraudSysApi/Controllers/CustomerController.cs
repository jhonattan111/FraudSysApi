using FraudSysApi.Models;
using FraudSysApi.Models.CustomerModels;
using FraudSysApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FraudSysApi.Controllers
{
    public class CustomerController(ICustomerService customerService) : GenericController
    {
        [HttpGet("{document}")]
        public async Task<IActionResult> GetByDocument(string document)
        {
            try
            {
                ApiResponse<CustomerResponse> response = await customerService.GetResponse(document);
                return StatusCode(response.StatusCode, new { response.Message, response.Data });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromBody] InsertCustomer customer)
        {
            try
            {
                ApiResponse<CustomerResponse> response = await customerService.Insert(customer);
                return StatusCode(response.StatusCode, new { response.Message, response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCustomer customer)
        {
            try
            {
                ApiResponse<CustomerResponse> response = await customerService.Update(customer);
                return StatusCode(response.StatusCode, new { response.Message, response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                ApiResponse<IEnumerable<CustomerResponse>> response = await customerService.ListAllCustomers();
                return StatusCode(response.StatusCode, new { response.Message, response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("[action]/{document}/{newPixLimitTransaction}")]
        public async Task<IActionResult> UpdatePixTransactionLimit(string newPixLimitTransaction, string document)
        {
            try
            {
                decimal.TryParse(newPixLimitTransaction, out decimal newPixTransactionParsed);
                newPixTransactionParsed /= 100;
                ApiResponse<string> response = await customerService.UpdatePixTransactionLimit(document, newPixTransactionParsed);
                return StatusCode(response.StatusCode, new { response.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("{document}")]
        public async Task<IActionResult> Delete(string document)
        {
            ApiResponse<string> response = await customerService.Delete(document);
            return Ok();
        }

        [HttpGet("[action]/{agencyNumber}/{accountNumber}")]
        public async Task<IActionResult> GetByAgencyNumberAccountNumber(string agencyNumber, string accountNumber)
        {
            try
            {
                ApiResponse<CustomerResponse> response = await customerService.GetByAgencyNumberAccountNumber(agencyNumber, accountNumber);
                return StatusCode(response.StatusCode, new { response.Message, response.Data });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
