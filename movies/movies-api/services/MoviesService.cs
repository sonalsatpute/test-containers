using Microsoft.Extensions.Options;
using MongoDB.Driver;
using movies_api.infrastructures.persistent;
using movies_api.infrastructures.persistent.entities;

namespace movies_api.services;

public class MoviesService
{
    private readonly IMongoCollection<Movie> _movies;

    public MoviesService(IOptions<MoviesDatabaseSettings> options)
    {
        var mongoClient = new MongoClient(options.Value.ConnectionString);

        _movies = mongoClient.GetDatabase(options.Value.DatabaseName)
            .GetCollection<Movie>(options.Value.MoviesCollectionName);
    }

    public async Task<List<Movie>> Get()
    {
        var filter = Builders<Movie>.Filter.Empty;
        
        List<Movie>? movies = await _movies.Find(filter).ToListAsync();
        
        return movies;
    }

    public async Task<Movie> Get(string id) =>
        await _movies.Find(m => m.Id == id).FirstOrDefaultAsync();

    public async Task Create(Movie newMovie) =>
        await _movies.InsertOneAsync(newMovie);

    public async Task Update(string id, Movie updateMovie) =>
        await _movies.ReplaceOneAsync(m => m.Id == id, updateMovie);

    public async Task Remove(string id) =>
        await _movies.DeleteOneAsync(m => m.Id == id);
}