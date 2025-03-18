using Amazon.DynamoDBv2.DataModel;

namespace FraudSysApi.Models.TransactionModels
{
    public class Transaction
    {
        [DynamoDBTable("sysfraud_transaction")]
        public class Customer
        {
            [DynamoDBHashKey("Id")]
            public string Id { get; set; }

            [DynamoDBProperty("ToDocument")]
            public string ToDocument { get; set; }

            [DynamoDBProperty("PixTransactionValue")]
            public decimal PixTransactionValue { get; set; }

            [DynamoDBProperty("PixTransactionLimitBefore")]
            public decimal PixTransactionLimitBefore { get; set; }

            [DynamoDBProperty("PixTransactionLimitAfter")]
            public decimal PixTransactionLimitAfter { get; set; }

            [DynamoDBProperty("CreatedAt")]
            public DateTimeOffset CreatedAt { get; set; }

            [DynamoDBGlobalSecondaryIndexHashKey("FromDocumentIndex", AttributeName = "FromDocument")]
            public string FromDocument { get; set; }
        }
    }

    public record TransactionResponse(string Id, string ToDocument, decimal PixTransactionValue, decimal PixTransactionLimitBefore, decimal PixTransactionLimitAfter, DateTimeOffset CreatedAt, string FromDocument);

}
