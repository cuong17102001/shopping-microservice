using Basket.API.Ropositories.Interfaces;
using Contracts.Common.Interfaces;
using EventBus.Messages.IntergrationEvents.Events;
using Infrastructure.Common;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Basket.API.Extensions;

public class EventBusSettings
{
    public string HostAddress { get; set; }
}

public static class ServiceExtensions
{

    internal static IServiceCollection ConfigurationSetting(this IServiceCollection services, IConfiguration config)
    {
        // Retrieve the HostAddress directly
        var hostAddress = config.GetSection("EventBusSettings").GetValue<string>("HostAddress");

        // Optionally, register the HostAddress as a singleton if you want to inject it elsewhere
        services.AddSingleton(new EventBusSettings { HostAddress = hostAddress });

        return services;
    }

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

    internal static IServiceCollection ConfigurationMassTransit(this IServiceCollection services, IConfiguration config)
    {
        var hostAddress = config.GetSection("EventBusSettings").GetValue<string>("HostAddress");

        var mqConnection = new Uri(hostAddress);
        services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(hostAddress, h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });

            config.AddRequestClient<BasketCheckoutEvent>();
        });

        return services;
    }
}