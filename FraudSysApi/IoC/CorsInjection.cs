namespace FraudSysApi.IoC
{
    public static class CorsInjection
    {
        public static void Inject(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowedUrls",
                    builder =>
                    {
                        builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
        }
    }
}
