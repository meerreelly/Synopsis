using Core;
using Core.DTO;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data_Services;

public class FilmService
{
    private readonly Repository<Film> _filmRepository;

    public FilmService(Repository<Film> filmRepository)
    {
        _filmRepository = filmRepository;
    }

    public IEnumerable<Film> GetAllFilms()
    {
        return _filmRepository.GetAllQueryable()
            .Include(f => f.Actors)
            .Include(f => f.Directors)
            .Include(f => f.Genres)
            .Include(f => f.Overviews);
    }

    public Film GetFilmById(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));

        var film = _filmRepository.GetAllQueryable()
            .Include(f => f.Actors)
            .Include(f => f.Directors)
            .Include(f => f.Genres)
            .Include(f => f.Overviews)
            .FirstOrDefault(f => f.FilmId == id);
        if (film == null) throw new Exception("Film not found");

        return film;
    }

    public void AddFilm(Film film)
    {
        if (film == null) throw new ArgumentNullException(nameof(film));
        _filmRepository.Add(film);
    }

    public void DeleteFilm(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));
        Film film = _filmRepository.GetById(id);
        if (film == null) throw new Exception("Film not found");
        _filmRepository.Delete(film);
    }

    public void AddFilmRange(IEnumerable<Film> films)
    {
        if (films == null) throw new ArgumentNullException(nameof(films));
        _filmRepository.AddRange(films);
    }

    public void UpdateFilm(Film film)
    {
        if (film == null) throw new ArgumentNullException(nameof(film));
        _filmRepository.Update(film);
    }

    public IEnumerable<Film> GetFilmRange(int page, int pageSize)
    {
        return  _filmRepository.GetAllQueryable()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }
    public IEnumerable<Film> GetFilmRange(int page, int pageSize, FilmSearchDto search)
    {
        var query = _filmRepository.GetAllQueryable();
        
        if (search is not null)
        {
            if (!string.IsNullOrEmpty(search.Title))
            {
                query = query.Where(f => f.Title.Contains(search.Title, StringComparison.OrdinalIgnoreCase));
            }

            if (search.TitleType is not  null &&search.TitleType.Name!="all")
            {
                query = query.Include(f=>f.TitleType)
                    .Where(f => f.TitleType.Name.ToLower() == search.TitleType.Name.ToLower());
            }

            if (search.ReleasedYear > 0)
            {
                query = query.Where(f => f.ReleasedYear == search.ReleasedYear);
            }

            if (search.Genres != null && search.Genres.Any())
            {
                query = query.Include(f=>f.Genres)
                    .Where(f => f.Genres.Any(g => search.Genres.Select(sg => sg.Name).Contains(g.Name)));
            }
        }
        
        return query.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }


    
    public int GetFilmCount()
    {
        return _filmRepository.GetAllQueryable().Count();
    }
}