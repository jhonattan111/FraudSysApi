using FluentValidation;
using FluentValidation.AspNetCore;
using FraudSysApi.IoC;

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
            CorsInjection.Inject(builder.Services);

            builder.Services.AddControllers();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            WebApplication app = builder.Build();

            //CustomerTable.CreateCustomerTableAsync(new AmazonDynamoDBClient(new AmazonDynamoDBConfig() {})).Wait();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowedUrls");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
