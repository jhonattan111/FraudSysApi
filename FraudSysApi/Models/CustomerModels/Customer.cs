﻿using Amazon.DynamoDBv2.DataModel;

namespace FraudSysApi.Models.CustomerModels
{
    [DynamoDBTable("sysfraud_customer")]
    public class Customer
    {
        [DynamoDBHashKey("pk")]
        public string Document { get; set; }

        [DynamoDBProperty("AgencyNumber")]
        public string AgencyNumber { get; set; }

        [DynamoDBProperty("AccountNumber")]
        public string AccountNumber { get; set; }

        [DynamoDBProperty("PixTransactionLimit")]
        public decimal PixTransactionLimit { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey("AgencyNumberAccountNumberIndex", AttributeName = "sk")]
        public string AgencyNumberAccountNumber => $"{AgencyNumber}-{AccountNumber}";
    }

    public record CustomerResponse(string Document, string AgencyNumber, string AccountNumber, decimal PixTransactionLimit);

}
