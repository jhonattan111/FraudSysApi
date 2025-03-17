using Amazon.DynamoDBv2.DataModel;

namespace FraudSysApi.Models.CustomerModels
{
    [DynamoDBTable("sysfraud_customer")]
    public class Customer
    {
        [DynamoDBHashKey]
        public string Id { get; set; }
        public string Document { get; set; }
        public string AgencyNumber { get; set; }
        public string AccountNumber { get; set; }
        public decimal PixTransactionLimit { get; set; }
    }

    public record CustomerResponse(string Id, string Document, string AgencyNumber, string AccountNumber, decimal PixTransactionLimit);

}
