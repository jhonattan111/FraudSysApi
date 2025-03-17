using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using FraudSysApi.Models.CustomerModels;
using FraudSysApi.Repositories.Interfaces;

namespace FraudSysApi.Repositories
{
    public class CustomerRepository(DynamoDBContext context) : ICustomerRepository
    {
        public async Task<CustomerResponse> Insert(InsertCustomer customer)
        {
            Customer addCustomer = new() {
                Document = customer.Document,
                AgencyNumber = customer.AgencyNumber,
                AccountNumber = customer.AccountNumber,
                PixTransactionLimit = customer.PixTransactionLimit
            };

            await context.SaveAsync(addCustomer);
            return new CustomerResponse(addCustomer.Document, addCustomer.AgencyNumber, addCustomer.AccountNumber, addCustomer.PixTransactionLimit);
        }

        public async Task<IEnumerable<CustomerResponse>> ListAllCustomers()
        {
            try
            {
                List<Customer> customers = await context.ScanAsync<Customer>([]).GetRemainingAsync();

                return customers.Select(c =>
                    new CustomerResponse(c.Document, c.AgencyNumber, c.AccountNumber, c.PixTransactionLimit));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
