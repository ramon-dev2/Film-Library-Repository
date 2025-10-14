namespace FilmLibrary.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public bool Liked { get; set; }
}