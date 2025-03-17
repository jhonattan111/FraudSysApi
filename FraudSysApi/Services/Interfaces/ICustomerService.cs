using FraudSysApi.Models.CustomerModels;

namespace FraudSysApi.Services.Shared
{
    public interface ICustomerService
    {
        Task<CustomerResponse> Insert(InsertCustomer customer);
        Task<IEnumerable<CustomerResponse>> ListAllCustomers();
    }
}
