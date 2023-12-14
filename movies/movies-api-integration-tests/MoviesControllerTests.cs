using FluentAssertions;
using Testcontainers.MongoDb;

namespace movies_api_integration_tests;

[TestFixture]
internal class MoviesControllerTests
{
    private CustomWebApplicationFactory _factory = null!;
    private HttpClient _client = null!;

    [SetUp]
    public async Task Setup()
    {
        // var container = new MongoDbBuilder()
        //     .WithImage("mongo:latest")
        //     .Build();
        //
        // await container.StartAsync();
        // string connectionString = container.GetConnectionString();

        _factory = new CustomWebApplicationFactory("connectionString");
        _client = _factory.CreateClient();
        _client.Timeout = TimeSpan.FromMinutes(2); // Set timeout to 2 minutes
    }
    
    [TearDown]
    public void TearDown()
    {
        _factory.Dispose();
        _client.Dispose();
    }
    
    
    [Test]
    public async Task METHOD()
    {
        // Arrange
        var responseMessage = await _client.GetAsync("api/Movies");
        
        // Act
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadAsStringAsync();
        response.Should().NotBeNullOrEmpty();
    }
}