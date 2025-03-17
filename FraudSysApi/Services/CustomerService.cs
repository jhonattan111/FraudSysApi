using FraudSysApi.Models.CustomerModels;
using FraudSysApi.Repositories.Interfaces;
using FraudSysApi.Services.Shared;

namespace FraudSysApi.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        public async Task<CustomerResponse> Insert(InsertCustomer customer)
        {
            return await customerRepository.Insert(customer);
        }

        public async Task<IEnumerable<CustomerResponse>> ListAllCustomers()
        {
            return await customerRepository.ListAllCustomers();
        }
    }
}
