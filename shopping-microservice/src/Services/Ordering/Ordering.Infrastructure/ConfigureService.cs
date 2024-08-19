using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure;
public static class ConfigureService
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<OrderingDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("OrderingConnection"),
            builder => builder.MigrationsAssembly(typeof(OrderingDbContext).Assembly.FullName)));

        return services;
    }
}