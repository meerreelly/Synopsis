namespace Core.DTO;

public class FilmSearchDto
{
    public string? Title { get; set; }
    public TitleType TitleType { get; set; }
    public int ReleasedYear { get; set; }
    public List<Genre> Genres { get; set; } = new List<Genre>();
}