using Inventory.Product.API.Services;
using Inventory.Product.API.Services.Interfaces;
using MongoDB.Driver;

namespace Inventory.Product.API.Extensions;

public class DatabaseSettings
{
    public string DBProvider {  get; set; }
    public string ConnectionString {  get; set; }
    public string DatabaseName { get; set; }
}

public static class ServiceExtension
{
    internal static IServiceCollection ConfigurationSetting(this IServiceCollection services, IConfiguration config)
    {
        // Retrieve the HostAddress directly
        var settings = config.GetSection("DatabaseSettings").Get<DatabaseSettings>();

        services.AddSingleton(settings);
        return services;
    }   

    private static string GetMongoConnectionString(IConfiguration config)
    {
        var connectionstring = config.GetSection("DatabaseSettings").GetValue<string>("ConnectionString");
        var databaseName = config.GetSection("DatabaseSettings").GetValue<string>("DatabaseName");

        var result = connectionstring + "/" + databaseName + "?authSource=admin";

        return result;
    }

    public static void ConfigMongoClient(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IMongoClient>(new MongoClient(GetMongoConnectionString(config)))
            .AddScoped(x => x.GetService<IMongoClient>()?.StartSession());
    }

    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
        services.AddScoped<IIventoryService, InventoryService>();
    }
}
