using FraudSysApi.Services;
using FraudSysApi.Services.Interfaces;
using FraudSysApi.Services.Shared;

namespace FraudSysApi.IoC
{
    public static class ServiceInjection
    {
        public static void Inject(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICustomerService, CustomerService>();
            builder.Services.AddScoped<ITransactionService, TransactionService>();
        }
    }
}
