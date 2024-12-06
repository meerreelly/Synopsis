using Core;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data_Services;

public class GenreService
{
    private readonly Repository<Genre> _genreRepository;

    public GenreService(Repository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public IEnumerable<Genre> GetAllGenres()
    {
        return _genreRepository.GetAllQueryable()
            .Include(g => g.Films);
    }

    public Genre GetGenreById(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));

        var genre = _genreRepository.GetAllQueryable()
            .Include(g => g.Films)
            .FirstOrDefault(g => g.GenreId == id);
        if (genre == null) throw new Exception("Genre not found");

        return genre;
    }

    public void AddGenre(Genre genre)
    {
        if (genre == null) throw new ArgumentNullException(nameof(genre));
        _genreRepository.Add(genre);
    }

    public void DeleteGenre(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));
        Genre genre = _genreRepository.GetById(id);
        if (genre == null) throw new Exception("Genre not found");
        _genreRepository.Delete(genre);
    }

    public void AddGenreRange(IEnumerable<Genre> genres)
    {
        if (genres == null) throw new ArgumentNullException(nameof(genres));
        _genreRepository.AddRange(genres);
    }

    public void UpdateGenre(Genre genre)
    {
        if (genre == null) throw new ArgumentNullException(nameof(genre));
        _genreRepository.Update(genre);
    }
}