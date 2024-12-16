namespace Core.DTO;

public class UserShortDto: Person
{
    public int UserId { get; set; }
    public string? AvatarUrl { get; set; }

    public UserShortDto() { }
    public UserShortDto(User user)
    {
        UserId = user.UserId;
        FirstName = user.FirstName;
        LastName = user.LastName;
        AvatarUrl = user.AvatarUrl;
    }
}