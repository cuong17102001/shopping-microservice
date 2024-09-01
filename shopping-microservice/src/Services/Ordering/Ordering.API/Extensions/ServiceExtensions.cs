using BuildingBlocks.Infrastructure.Configurations;
using MassTransit;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Ordering.API.Application.IntergrationEvents.EventHandlers;

namespace Ordering.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddConfigurationSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var emailSettings = configuration.GetSection(nameof(SMTPEmailSettings)).Get<SMTPEmailSettings>();
        services.AddSingleton(emailSettings);

        return services;
    }

    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        var hostAddress = configuration.GetSection("EventBusSettings").GetValue<string>("HostAddress");

        var mqConnection = new Uri(hostAddress);
        services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
        services.AddMassTransit(config =>
        {
            config.AddConsumersFromNamespaceContaining<BasketCheckoutEventHandler>();
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(hostAddress, h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ConfigureEndpoints(ctx);
            });
        });

        return services;
    }
}