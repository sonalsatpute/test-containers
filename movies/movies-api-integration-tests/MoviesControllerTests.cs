using FluentAssertions;

namespace movies_api_integration_tests;

[TestFixture]
internal class MoviesControllerTests
{
    private CustomWebApplicationFactory _factory = null!;
    private HttpClient _client = null!;

    // [SetUp]
    // public void Setup()
    // {
    //     _factory = new CustomWebApplicationFactory();
    //     _client = _factory.CreateClient();
    //     _client.Timeout = TimeSpan.FromMinutes(2); // Set timeout to 2 minutes
    // }
    
    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        Console.SetOut(TestContext.Progress);
        Console.SetError(TestContext.Error);
        
        _factory = new CustomWebApplicationFactory();
        _client = _factory.CreateClient();
    }
    
    [TearDown]
    public void TearDown()
    {
        _factory.Dispose();
        _client.Dispose();
    }
    
    [Test]
    public async Task GetMovies_ShouldReturnMovies() 
    {
        var responseMessage = await _client.GetAsync("api/Movies");
        
        responseMessage.EnsureSuccessStatusCode();
        var response = await responseMessage.Content.ReadAsStringAsync();
        response.Should().NotBeNullOrEmpty();
    }
}