using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services, handlers, validators, etc.
            // services.AddScoped<IOrderService, OrderService>();
            return services;
        }
    }
}
