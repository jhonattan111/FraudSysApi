using FraudSysApi.Models.CustomerModels;
using FraudSysApi.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace FraudSysApi.Controllers
{
    public class CustomerController(ICustomerService customerService) : GenericController
    {
        [HttpGet("[action]")]
        public IActionResult GetById()
        {
            try
            {

                //
                //customerService.Insert();
                return Ok();
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
                CustomerResponse response = await customerService.Insert(customer);
                return Ok(response);
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
                IEnumerable<CustomerResponse> customers = await customerService.ListAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("[action]/{customerId}")]
        public async Task<IActionResult> UpdatePixTransactionLimit([FromQuery] decimal newPixLimitTransaction)
        {
            return Ok();
        }

        [HttpDelete()]
        public async Task<IActionResult> RemoveCustomer()
        {
            return Ok();
        }
    }
}
