namespace Core;

public class User: Person
{
    public int UserId { get; set; }
    public string? Email { get; set; }
    public string? AvatarUrl { get; set; } = "default.png";
    public string? GoogleId { get; set; }
    public string? Role { get; set; } = "user";
    public string? Password { get; set; }
    public string? DiscordId { get; set; }
    
    public ICollection<Overview> Overviews { get; set; } = new List<Overview>();
    public ICollection<FilmPoint> FilmPoints { get; set; } = new List<FilmPoint>();
    public ICollection<ViewStatus> ViewStatuses { get; set; } = new List<ViewStatus>();
}