using FraudSysApi.Models;
using FraudSysApi.Models.CustomerModels;

namespace FraudSysApi.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerResponse> Insert(InsertCustomer customer);
        Task<CustomerResponse> Update(UpdateCustomer customer, Customer customerModel);
        Task<IEnumerable<CustomerResponse>> ListAllCustomers();
        Task<Customer> GetModel(string document);
        Task<bool> UpdatePixTransactionLimit(string document, decimal newPixLimitTransaction);
        Task<CustomerResponse> GetByAgencyNumberAccountNumber(string agencyNumber, string accountNumber);
        Task<bool> Delete(string document);
    }
}
