using Contracts.Common.Interfaces;
using Infrastructure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Common.Interfaces;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repositories;

namespace Ordering.Infrastructure;
public static class ConfigureService
{
    public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<OrderingDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("OrderingConnection"),
            builder => builder.MigrationsAssembly(typeof(OrderingDbContext).Assembly.FullName)));

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

        return services;
    }
}