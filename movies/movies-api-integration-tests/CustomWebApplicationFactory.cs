using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using movies_api.infrastructures.persistent;
using movies_api.services;
using Testcontainers.MongoDb;

namespace movies_api_integration_tests;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var container = new MongoDbBuilder()
            .WithImage("mongo:latest")
            .Build();

        builder.ConfigureServices(services =>
        {
            var task = Task.Factory.StartNew(async () =>
            {
                await container.StartAsync();
            }).Unwrap();

            task.Wait();
            string connectionString = container.GetConnectionString();
            
            var service = services.SingleOrDefault(d => d.ServiceType == typeof(MoviesService));
            services.Remove(service!);

            services.Configure<MoviesDatabaseSettings>(options =>
            {
                options.ConnectionString = connectionString;
                options.DatabaseName = "moviesdb";
                options.MoviesCollectionName = "Movies";
            });

            services.AddSingleton<MoviesService>();
            
        });
        
        builder.UseEnvironment("Development");
    }
}