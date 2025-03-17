using FluentValidation;
using FraudSysApi.IoC;
using FraudSysApi.Models.CustomerModels;

namespace FraudSysApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            ServiceInjection.Inject(builder);
            RepositoryInjection.Inject(builder);
            DatabaseInjection.Inject(builder);

            builder.Services.AddScoped<IValidator<InsertCustomer>, InsertCustomerValidator>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
