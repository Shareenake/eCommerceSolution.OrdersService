

using Amazon.Util;
using eCommerce.OrderService.DataAccessLayer.Repositories;
using eCommerce.OrderService.DataAccessLayer.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace eCommerce.OrderService.DataAccessLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection Services,IConfiguration configuration )
    {
        //TO DO: Add data accesss layer services into the IOC container
        string connectionStringTemplate = configuration.GetConnectionString("MongoDB")!;
        string connectionString = connectionStringTemplate
            .Replace("$MONGO_HOST", Environment
            .GetEnvironmentVariable("MONGODB_HOST"))
            .Replace("$MONGO_PORT", Environment.GetEnvironmentVariable("MONGODB_PORT"));

        Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

        Services.AddScoped<IMongoDatabase>(provider =>
        {
            IMongoClient client=provider.GetRequiredService<IMongoClient>();
            return client.GetDatabase(Environment.GetEnvironmentVariable("MONGODB_DATABASE"));
        });

        Services.AddScoped<IOrderRepository, OrderRepository>();
        return Services;
    }
}
