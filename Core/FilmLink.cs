using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public class FilmLink
{
    public int FilmLinkId { get; set; } 
    public string? Url { get; set; }
    public string Language { get; set; } = "en";
    
    [ForeignKey(nameof(Film))]
    public int FilmId { get; set; } 
    public Film? Film { get; set; }
}