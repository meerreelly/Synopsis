using Core;
using Core.DTO;
using Microsoft.AspNetCore.Mvc;
using Repository.Data_Services;
using Repository.Domain;
using Repository.Repositories;

namespace SynopsisAPI.Controllers;

[ApiController]
[Route("FilmSystem")]
public class FilmController(Context ctx, IConfiguration configuration): ControllerBase
{
    [HttpPost("/getRange")]
    public IActionResult GetRange(int page, int count, FilmSearchDto? search)
    {
        using (var rep = new Repository<Film>(ctx))
        {
            var ds = new FilmService(rep);
            if(search != null)
            {
                var films = ds.GetFilmRange(page, count, search);
                return Ok(films);
            }else
            {
                var films = ds.GetFilmRange(page, count);
                return Ok(films);
            }
            
        }
    }
    [HttpPost("/addFilm")]
    public IActionResult AddFilm(Film film)
    {
        using (var rep = new Repository<Film>(ctx))
        {
            var ds = new FilmService(rep);
            ds.AddFilm(film);
            return Ok();
        }
    }
    
    [HttpGet("/getFilmCount")]
    public IActionResult GetFilmCount()
    {
        using (var rep = new Repository<Film>(ctx))
        {
            var ds = new FilmService(rep);
            var count = ds.GetFilmCount();
            return Ok(count);
        }
    }
    [HttpGet("/getFilm")]
    public IActionResult GetFilm(int id)
    {
        using (var rep = new Repository<Film>(ctx))
        {
            var ds = new FilmService(rep);
            var film = ds.GetFilmById(id);
            return Ok(film);
        }
    }
}