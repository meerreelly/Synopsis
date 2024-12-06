using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public class Overview
{
    public int OverviewId { get; set; } 
    public string? UserOverview { get; set; }
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public User? User { get; set; }
    
    [ForeignKey(nameof(Film))]
    public int FilmId { get; set; } 
    public Film? Film { get; set; }
}
