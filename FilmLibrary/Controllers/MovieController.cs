using FilmLibrary.Entities;
using FilmLibrary.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FilmLibrary.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController(ILogger<MovieController> logger, FilmLibraryContext context) : ControllerBase
{
    private readonly ILogger<MovieController> _logger = logger;
    private readonly FilmLibraryContext _context = context;

    [HttpGet]
    public IEnumerable<Movie> GetMovie([FromQuery] MovieFilter filter)
    {
        var movies = _context.Movies
            .Where(x => string.IsNullOrEmpty(filter.Title) || x.Title.Contains(filter.Title))
            .Where(x => filter.Wishlist  == x.Liked || !filter.Wishlist == !x.Liked)
            .ToList();
        return movies;
    }

    [HttpPost]
    public IActionResult Post(Movie movie)
    {
        var movies = new Movie
        {
            Title = movie.Title,
            ReleaseDate = movie.ReleaseDate,
            Liked = movie.Liked
        };
        
        _context.Movies.Add(movies);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, Movie movie)
    {
        if (id != movie.Id)
        {
            return BadRequest();
        }

        var existingMovie = _context.Movies.Find(id);
        if (existingMovie == null)
        {
            return NotFound();
        }

        existingMovie.Title = movie.Title;
        existingMovie.ReleaseDate = movie.ReleaseDate;
        existingMovie.Liked = movie.Liked;

        _context.SaveChanges();
        return NoContent();
    }
}