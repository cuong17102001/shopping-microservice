using System.Reflection;
using BuildingBlocks.Contracts.Services;
using BuildingBlocks.Shared.Services.Email;
using FluentValidation;
using Infrastructure.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Common.Behaviours;
using Ordering.Application.Feature.V1.Orders.Commands.CreateOrder;
using Ordering.Domain.Entities;

namespace Ordering.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnHandledExceptionBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddSingleton<IEmailService<MailRequest>, SmtpEmailService>();
        return services;
    }
}