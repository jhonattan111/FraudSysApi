using Amazon.DynamoDBv2.DataModel;

namespace FraudSysApi.Models.CustomerModels
{
    [DynamoDBTable("sysfraud_customer_log")]
    public class CustomerLog
    {
        [DynamoDBHashKey("Document")]
        public string Document { get; set; }

        [DynamoDBProperty("AgencyNumber")]
        public string AgencyNumber { get; set; }

        [DynamoDBProperty("AccountNumber")]
        public string AccountNumber { get; set; }

        [DynamoDBProperty("PixTransactionLimit")]
        public decimal PixTransactionLimit { get; set; }

    }
}
