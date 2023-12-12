using Microsoft.AspNetCore.Mvc;
using movies_api.infrastructures.persistent.entities;
using movies_api.services;

namespace movies_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController : ControllerBase
{
    private readonly MoviesService _moviesService;

    public MoviesController(MoviesService moviesService)
    {
        _moviesService = moviesService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _moviesService.Get();
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id) => Ok(await _moviesService.Get(id));

    [HttpPost]
    public async Task<IActionResult> Create(Movie movie)
    {
        await _moviesService.Create(movie);
        return Ok();
    }
    
    [HttpPut("{id}")]   
    public async Task<IActionResult> Update(string id, Movie updateMovie)
    {
        var movie = await _moviesService.Get(id);
        if (movie == null) return NotFound();

        updateMovie.Id = movie.Id;
        await _moviesService.Update(id, updateMovie);

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(string id)
    {
        var movie = await _moviesService.Get(id);
        if (movie is null) return NotFound();

        await _moviesService.Remove(movie.Id);

        return NoContent();
    }
}