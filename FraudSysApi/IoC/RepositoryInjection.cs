using FraudSysApi.Repositories;
using FraudSysApi.Repositories.Interfaces;

namespace FraudSysApi.IoC
{
    public static class RepositoryInjection
    {
        public static void Inject(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
        }
    }
}
