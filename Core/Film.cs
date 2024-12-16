namespace Core;

public class Film
{
    public int FilmId { get; set; } 
    public string? PosterUrl { get; set; }
    public string? Title { get; set; }
    public TitleType TitleType { get; set; }
    public int ReleasedYear { get; set; }
    public string? Certificate { get; set; }
    public int Runtime { get; set; }
    public double ImdbRating { get; set; }
    public string? ShortStory { get; set; }
    public int MetaScore { get; set; }
    public int NumberOfVotes { get; set; }
    public double Gross { get; set; }

    public ICollection<Actor> Actors { get; set; } = new List<Actor>();
    public ICollection<Director> Directors { get; set; } = new List<Director>();
    public ICollection<Genre> Genres { get; set; } = new List<Genre>();
    public ICollection<Overview> Overviews { get; set; } = new List<Overview>();
}
