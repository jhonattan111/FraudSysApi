using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using FraudSysApi.Models;
using FraudSysApi.Models.CustomerModels;
using FraudSysApi.Repositories.Interfaces;
using System.Collections.Concurrent;
using System.Globalization;

namespace FraudSysApi.Repositories
{
    public class CustomerRepository(DynamoDBContext context, IAmazonDynamoDB client) : ICustomerRepository
    {
        private readonly string TableName = "sysfraud_customer";

        public async Task<Customer> GetModel(string document)
        {
            return await context.LoadAsync<Customer>(document);
        }

        public async Task<CustomerResponse> Insert(InsertCustomer customer)
        {
            Customer addCustomer = new()
            {
                Document = customer.Document,
                AgencyNumber = customer.AgencyNumber,
                AccountNumber = customer.AccountNumber,
                PixTransactionLimit = customer.PixTransactionLimit,
                AgencyNumberAccountNumber = $"{customer.AgencyNumber}-{customer.AccountNumber}"
            };

            await context.SaveAsync(addCustomer);
            return new CustomerResponse(addCustomer.Document, addCustomer.AgencyNumber, addCustomer.AccountNumber, addCustomer.PixTransactionLimit);
        }

        public async Task<CustomerResponse> Update(UpdateCustomer customer, Customer customerModel)
        {
            UpdateItemRequest request = new()
            {
                TableName = TableName,
                Key = new() {
                    { "Document", new AttributeValue { S = customer.Document} }
                },
                UpdateExpression = "SET AgencyNumber = :AgencyNumber, AccountNumber = :AccountNumber",
                ExpressionAttributeValues = new()
                {
                    { ":AgencyNumber", new() { N = customer.AgencyNumber } },
                    { ":AccountNumber", new() { N = customer.AccountNumber } },
                }
            };

            UpdateItemResponse updatedItem = await client.UpdateItemAsync(request);
            return new CustomerResponse(customer.Document, customer.AgencyNumber, customer.AccountNumber, customerModel.PixTransactionLimit);
        }

        public async Task<bool> Delete(string document)
        {
            try
            {
                var deleteRequest = new DeleteItemRequest
                {
                    TableName = TableName,
                    Key = new Dictionary<string, AttributeValue>
                    {
                        { "Document", new AttributeValue { S = document } }
                }
                };

                var response = await client.DeleteItemAsync(deleteRequest);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (Exception)
            {
                return false;
            }
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

        public async Task<bool> UpdatePixTransactionLimit(string document, decimal newPixLimitTransaction)
        {
            UpdateItemRequest request = new()
            {
                TableName = TableName,
                Key = new() {
                    { "Document", new AttributeValue { S = document} }
                },
                UpdateExpression = "SET PixTransactionLimit = :PixTransactionLimit",
                ExpressionAttributeValues = new()
                {
                    { ":PixTransactionLimit", new() { N = newPixLimitTransaction.ToString("0.##", System.Globalization.CultureInfo.InvariantCulture) } }
                }
            };

            UpdateItemResponse updatedItem = await client.UpdateItemAsync(request);

            bool success = (int) updatedItem.HttpStatusCode == 200;
            return success;
        }

        public async Task<CustomerResponse> GetByAgencyNumberAccountNumber(string agencyNumber, string accountNumber)
        {
            string agencyAccountKey = $"{agencyNumber}-{accountNumber}";

            QueryRequest request = new()
            {
                TableName = TableName,
                IndexName = "AgencyNumberAccountNumberIndex",
                KeyConditionExpression = "AgencyNumberAccountNumber = :AgencyAccountKey",
                ExpressionAttributeValues = new()
                {
                    { ":AgencyAccountKey", new AttributeValue { S = agencyAccountKey } }
                }
            };

            QueryResponse response = await client.QueryAsync(request);

            if (response.Items.Count == 0)
                return null;

            Customer customer = context.FromDocument<Customer>(Document.FromAttributeMap(response.Items[0]));
            return new CustomerResponse(customer.Document, customer.AgencyNumber, customer.AccountNumber, customer.PixTransactionLimit);
        }
    }
}
