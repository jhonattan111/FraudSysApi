using Amazon.DynamoDBv2.DataModel;

namespace FraudSysApi.Models.CustomerModels
{
    [DynamoDBTable("sysfraud_customer")]
    public class Customer
    {
        [DynamoDBHashKey("pk")]
        public required string Document { get; set; }
        public required string AgencyNumber { get; set; }
        public required string AccountNumber { get; set; }

        [DynamoDBRangeKey("sk")]
        public string AgencyNumberAccountNumber { get => $"{AgencyNumber}-{AccountNumber}"; }
        
        public decimal PixTransactionLimit { get; set; }
    }

    public record CustomerResponse(string Id, string Document, string AgencyNumber, string AccountNumber, decimal PixTransactionLimit);

}
