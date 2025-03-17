using FraudSysApi.Repositories;
using FraudSysApi.Repositories.Interfaces;

namespace FraudSysApi.IoC
{
    public class RepositoryInjection
    {
        public static void Inject(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
