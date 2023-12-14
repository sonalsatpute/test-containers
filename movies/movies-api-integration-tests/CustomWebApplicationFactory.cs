using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using movies_api.infrastructures.persistent;
using movies_api.services;

namespace movies_api_integration_tests;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public CustomWebApplicationFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var service = services.SingleOrDefault(d => d.ServiceType == typeof(MoviesService));
            services.Remove(service!);

            services.Configure<MoviesDatabaseSettings>(options =>
            {
                options.ConnectionString = _connectionString;
                options.DatabaseName = "moviesdb";
                options.MoviesCollectionName = "Movies";
            });

            services.AddSingleton<MoviesService>();
        });
        builder.UseEnvironment("Development");
    }

    private static void InitDatabase(string connectionString, string databaseName, string collectionName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _ = database.GetCollection<BsonDocument>(collectionName);
    }
}