namespace Core.DTO;

public class UserInfoDto:Person
{
    public int UserId { get; set; }
    public string? Email { get; set; }
    public string? AvatarUrl { get; set; } = "default.png";
    
    public ICollection<Overview> Overviews { get; set; } = new List<Overview>();
    public ICollection<FilmPoint> FilmPoints { get; set; } = new List<FilmPoint>();
    public ICollection<ViewStatus> ViewStatuses { get; set; } = new List<ViewStatus>();
    
    public UserInfoDto(User user)
    {
        UserId = user.UserId;
        Email = user.Email;
        AvatarUrl = user.AvatarUrl;
        Overviews = user.Overviews;
        FilmPoints = user.FilmPoints;
        ViewStatuses = user.ViewStatuses;
    }

    public UserInfoDto()
    {
        
    }
}