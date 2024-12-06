namespace Core;

public class Director : Person
{
    public int DirectorId { get; set; }
    
    public ICollection<Film> Films { get; set; } = new List<Film>();
}
