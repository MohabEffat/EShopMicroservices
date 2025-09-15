using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            //// Add database context
            //services.AddDbContext<OrderingDbContext>(options =>
            //    options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));
            //// Add repositories
            //services.AddScoped<IOrderRepository, OrderRepository>();
            //// Add other infrastructure services as needed
            return services;
        }
    }
}
