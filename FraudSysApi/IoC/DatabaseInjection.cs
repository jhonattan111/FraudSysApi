using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.Runtime;
using Amazon;

namespace FraudSysApi.IoC
{
    internal static class DatabaseInjection
    {
        public static void Inject(WebApplicationBuilder builder)
        {
            IConfiguration configuration = builder.Configuration;

            BasicAWSCredentials credentials = new(
                configuration["FraudSysDB:KeyId"],
                configuration["FraudSysDB:AccessKey"]
            );

            AmazonDynamoDBClient client = new(credentials, new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.USEast1,
                ServiceURL = configuration["FraudSysDB:ServiceUrl"]
            });

            builder.Services.AddSingleton<IAmazonDynamoDB>(client);

            builder.Services.AddScoped<DynamoDBContext>(sp => new DynamoDBContext(sp.GetRequiredService<IAmazonDynamoDB>()));
        }
    }
}
