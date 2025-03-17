using FraudSysApi.Models;
using FraudSysApi.Models.CustomerModels;
using FraudSysApi.Models.Enums;
using FraudSysApi.Repositories.Interfaces;
using FraudSysApi.Services.Interfaces;

namespace FraudSysApi.Services
{
    public class CustomerService(ICustomerRepository customerRepository) : ICustomerService
    {
        public async Task<ApiResponse<CustomerResponse>> Insert(InsertCustomer customer)
        {
            Customer customerModel = await GetModel(customer.Document);
            if (customerModel is not null) return ApiResponse<CustomerResponse>.Error("customerAlreadyExists", StatusCodes.Status204NoContent);

            CustomerResponse insertedCustomer = await customerRepository.Insert(customer);
            return ApiResponse<CustomerResponse>.Success(insertedCustomer);
        }

        public async Task<ApiResponse<IEnumerable<CustomerResponse>>> ListAllCustomers()
        {
            IEnumerable<CustomerResponse> customers = await customerRepository.ListAllCustomers();

            if (customers is null) return ApiResponse<IEnumerable<CustomerResponse>>.Error("notFounded", StatusCodes.Status404NotFound);

            return ApiResponse<IEnumerable<CustomerResponse>>.Success(customers);
        }

        public async Task<Customer> GetModel(string document)
        {
            Customer customer = await customerRepository.GetModel(document);
            return customer;
        }

        public async Task<ApiResponse<CustomerResponse>> GetResponse(string document)
        {
            Customer customer = await GetModel(document);
            if (customer is null) return ApiResponse<CustomerResponse>.Error("notFounded", StatusCodes.Status404NotFound);

            CustomerResponse response = new(customer.Document, customer.AgencyNumber, customer.AccountNumber, customer.PixTransactionLimit);
            return ApiResponse<CustomerResponse>.Success(response);
        }

        public async Task<ApiResponse<string>> UpdatePixTransactionLimit(string document, decimal newPixLimitTransaction)
        {
            Customer customer = await GetModel(document);

            if (customer is null) return ApiResponse<string>.Error(PixTransactionLimitStates.CUSTOMER_NOT_FOUNDED.ToString(), StatusCodes.Status406NotAcceptable);
            if (customer.PixTransactionLimit == newPixLimitTransaction) return ApiResponse<string>.Error(PixTransactionLimitStates.SAME_LIMIT.ToString(), StatusCodes.Status406NotAcceptable);

            await customerRepository.UpdatePixTransactionLimit(document, newPixLimitTransaction);
            return ApiResponse<string>.Success(PixTransactionLimitStates.UPDATED_LIMIT.ToString());
        }

        public async Task<ApiResponse<CustomerResponse>> GetByAgencyNumberAccountNumber(string agencyNumber, string accountNumber)
        {
            CustomerResponse customer = await customerRepository.GetByAgencyNumberAccountNumber(agencyNumber, accountNumber);

            if (customer is null) return ApiResponse<CustomerResponse>.Error("notFounded", StatusCodes.Status404NotFound);

            return ApiResponse<CustomerResponse>.Success(customer);
        }
    }
}
