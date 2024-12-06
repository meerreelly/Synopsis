using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public class ViewStatus
{
    public int ViewStatusId { get; set; }
    public Statuses? Status { get; set; } 
    
    [ForeignKey(nameof(User))]
    public int UserId { get; set; } 
    public User? User { get; set; }
    
    [ForeignKey(nameof(Film))]
    public int FilmId { get; set; } 
    public Film? Film { get; set; }
}
