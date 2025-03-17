using FraudSysApi.Models.CustomerModels;

namespace FraudSysApi.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<CustomerResponse> Insert(InsertCustomer customer);
        Task<IEnumerable<CustomerResponse>> ListAllCustomers();
    }
}
