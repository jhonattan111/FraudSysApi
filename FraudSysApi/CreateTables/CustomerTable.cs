using System.Linq.Expressions;
using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;

namespace FraudSysApi.CreateTables
{
    public static class CustomerTable
    {
        public static async Task<bool> CreateCustomerTableAsync(IAmazonDynamoDB client)
        {
            try
            {
                CreateTableResponse? request = await client.CreateTableAsync(new CreateTableRequest
                {
                    TableName = "sysfraud_customer",
                    AttributeDefinitions =
                    [
                        new()
                        {
                            AttributeName = "document",
                            AttributeType = ScalarAttributeType.S
                        },
                        new()
                        {
                            AttributeName = "agency_number_account_number",
                            AttributeType = ScalarAttributeType.S
                        }
                    ],
                    KeySchema =
                    [
                        new KeySchemaElement
                        {
                            AttributeName = "Document",
                            KeyType = KeyType.HASH
                        }
                    ],
                    ProvisionedThroughput = new()
                    {
                        ReadCapacityUnits = 5,
                        WriteCapacityUnits = 5
                    },
                    GlobalSecondaryIndexes = new System.Collections.Generic.List<GlobalSecondaryIndex>
                    {
                        new GlobalSecondaryIndex
                        {
                            IndexName = "agency_number_account_number_index",
                            KeySchema =
                            [
                                new KeySchemaElement
                                {
                                    AttributeName = "AgencyNumberAccountNumber",
                                    KeyType = KeyType.HASH
                                }
                            ],
                            Projection = new Projection
                            {
                                ProjectionType = ProjectionType.ALL
                            },
                            ProvisionedThroughput = new ProvisionedThroughput
                            {
                                ReadCapacityUnits = 5,
                                WriteCapacityUnits = 5
                            }
                        }
                    }
                });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
