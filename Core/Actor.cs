namespace Core;

public class Actor : Person
{
    public int ActorId { get; set; }
    
    public ICollection<Film> Films { get; set; } = new List<Film>();
}
