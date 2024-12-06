using Core;
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
}