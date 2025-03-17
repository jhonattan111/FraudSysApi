using FraudSysApi.Models;
using FraudSysApi.Models.CustomerModels;

namespace FraudSysApi.Services.Interfaces;

public interface ICustomerService
{
    Task<ApiResponse<CustomerResponse>> Insert(InsertCustomer customer);
    Task<ApiResponse<IEnumerable<CustomerResponse>>> ListAllCustomers();
    Task<Customer> GetModel(string document);
    Task<ApiResponse<CustomerResponse>> GetResponse(string document);
    Task<ApiResponse<string>> UpdatePixTransactionLimit(string document, decimal newPixLimitTransaction);
    Task<ApiResponse<CustomerResponse>> GetByAgencyNumberAccountNumber(string agencyNumber, string accountNumber);
}
