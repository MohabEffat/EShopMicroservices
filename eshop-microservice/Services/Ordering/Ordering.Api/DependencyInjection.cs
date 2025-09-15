namespace Ordering.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            // Register API-specific services, controllers, etc.
            // services.AddControllers();
            return services;
        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            // Configure the HTTP request pipeline, middleware, etc.
            // app.UseRouting();
            // app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            return app;
        }
    }
}
