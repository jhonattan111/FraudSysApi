using FraudSysApi.Services;
using FraudSysApi.Services.Shared;

namespace FraudSysApi.IoC
{
    public class ServiceInjection
    {
        public static void Inject(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICustomerService, CustomerService>();
        }
    }
}
