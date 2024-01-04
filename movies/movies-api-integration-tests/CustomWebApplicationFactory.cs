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
        Console.WriteLine("ConfigureWebHost: creating container");
        
        var container = new MongoDbBuilder()
            .WithImage("mongo:3.6")
            .Build();
        Console.WriteLine("ConfigureWebHost: container created");
        
        builder.ConfigureServices(services =>
        {

            Console.WriteLine("ConfigureWebHost: configuring services");
            
            var task = Task.Factory.StartNew(async () =>
            {
                await container.StartAsync();
            }).Unwrap();

            task.Wait();

            Console.WriteLine("ConfigureWebHost: container started");
            
            string connectionString = container.GetConnectionString();
            Console.WriteLine(connectionString);
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