using Amazon.DynamoDBv2.Model;
using Amazon.DynamoDBv2;

namespace FraudSysApi.CreateTables
{
    public static class CustomerTable
    {
        public static async Task<bool> CreateCustomerTableAsync(AmazonDynamoDBClient client, string tableName)
        {
            var response = await client.CreateTableAsync(new CreateTableRequest
            {
                TableName = tableName,
                AttributeDefinitions =
                [
                    new()
                    {
                        AttributeName = "id",
                        AttributeType = ScalarAttributeType.S,
                    },
                    new()
                    {
                        AttributeName = "document",
                        AttributeType = ScalarAttributeType.S,
                    },
                ],
                KeySchema =
                [
                    new KeySchemaElement
                    {
                        AttributeName = "id",
                        KeyType = KeyType.HASH,
                    },
                    new KeySchemaElement
                    {
                        AttributeName = "document",
                        KeyType = KeyType.RANGE,
                    },
                ]
            });

            var request = new DescribeTableRequest
            {
                TableName = response.TableDescription.TableName,
            };

            TableStatus status;

            do
            {
                DescribeTableResponse describeTableResponse = await client.DescribeTableAsync(request);
                status = describeTableResponse.Table.TableStatus;
            }
            while (status != "ACTIVE");

            return status == TableStatus.ACTIVE;
        }
    }
}
