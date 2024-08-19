using Basket.API.Ropositories.Interfaces;
using Contracts.Common.Interfaces;
using Infrastructure.Common;

namespace Basket.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IBasketRepository, Ropositories.BasketRepository>();
        services.AddTransient<ISerializeService, SerializeService>();

        return services;
    }

    public static void ConfigureRedis(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConnectionString = configuration.GetSection("CacheSettings:ConnectionString").Value;
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = redisConnectionString;
        });
    }
}