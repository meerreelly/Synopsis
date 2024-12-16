using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace CsvParser;

public class FilmRaw
{
    [Name("Poster_Link")]
    public string? PosterUrl { get; set; }

    [Name("Series_Title")]
    public string? Title { get; set; }

    [Name("Released_Year")]
    public int ReleasedYear { get; set; }

    [Name("Certificate")]
    public string? Certificate { get; set; }

    [Name("Runtime")]
    public string? Runtime { get; set; }

    [Name("Genre")]
    public string? Genre { get; set; }

    [Name("IMDB_Rating")]
    public double ImdbRating { get; set; }

    [Name("Overview")]
    public string? Overview { get; set; }

    [Name("Meta_score")]
    public int? MetaScore { get; set; }

    [Name("Director")]
    public string? Director { get; set; }

    [Name("Star1")]
    public string? Star1 { get; set; }

    [Name("Star2")]
    public string? Star2 { get; set; }

    [Name("Star3")]
    public string? Star3 { get; set; }

    [Name("Star4")]
    public string? Star4 { get; set; }

    [Name("No_of_Votes")]
    public int NumberOfVotes { get; set; }

    [Name("Gross")]
    public string? Gross { get; set; }
}

public class FilmMap : ClassMap<FilmRaw>
{
    public FilmMap()
    {
        Map(m => m.PosterUrl).Name("Poster_Link");
        Map(m => m.Title).Name("Series_Title");
        Map(m => m.ReleasedYear).Name("Released_Year");
        Map(m => m.Certificate).Name("Certificate");
        Map(m => m.Runtime).Name("Runtime");
        Map(m => m.Genre).Name("Genre");
        Map(m => m.ImdbRating).Name("IMDB_Rating");
        Map(m => m.Overview).Name("Overview");
        Map(m => m.MetaScore).Name("Meta_score");
        Map(m => m.Director).Name("Director");
        Map(m => m.Star1).Name("Star1");
        Map(m => m.Star2).Name("Star2");
        Map(m => m.Star3).Name("Star3");
        Map(m => m.Star4).Name("Star4");
        Map(m => m.NumberOfVotes).Name("No_of_Votes");
        Map(m => m.Gross).Name("Gross");
    }
}
