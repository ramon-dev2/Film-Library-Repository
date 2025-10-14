using FilmLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmLibrary;

public class FilmLibraryContext(DbContextOptions<FilmLibraryContext> options) : DbContext(options)
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Actor> Actors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Movie>().ToTable("Movies");
        modelBuilder.Entity<Actor>().ToTable("Actors");
        base.OnModelCreating(modelBuilder);
    }
}