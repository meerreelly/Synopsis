using Core;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data_Services;

public class FilmPointService
{
    private readonly Repository<FilmPoint> _filmPointRepository;

    public FilmPointService(Repository<FilmPoint> filmPointRepository)
    {
        _filmPointRepository = filmPointRepository;
    }

    public IEnumerable<FilmPoint> GetAllFilmPoints()
    {
        return _filmPointRepository.GetAllQueryable()
            .Include(fp => fp.Film)
            .Include(fp => fp.User);
    }

    public FilmPoint GetFilmPointById(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));

        var filmPoint = _filmPointRepository.GetAllQueryable()
            .Include(fp => fp.Film)
            .Include(fp => fp.User)
            .FirstOrDefault(fp => fp.FilmPointId == id);
        if (filmPoint == null) throw new Exception("FilmPoint not found");

        return filmPoint;
    }

    public void AddFilmPoint(FilmPoint filmPoint)
    {
        if (filmPoint == null) throw new ArgumentNullException(nameof(filmPoint));
        _filmPointRepository.Add(filmPoint);
    }

    public void DeleteFilmPoint(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));
        FilmPoint filmPoint = _filmPointRepository.GetById(id);
        if (filmPoint == null) throw new Exception("FilmPoint not found");
        _filmPointRepository.Delete(filmPoint);
    }

    public void AddFilmPointRange(IEnumerable<FilmPoint> filmPoints)
    {
        if (filmPoints == null) throw new ArgumentNullException(nameof(filmPoints));
        _filmPointRepository.AddRange(filmPoints);
    }

    public void UpdateFilmPoint(FilmPoint filmPoint)
    {
        if (filmPoint == null) throw new ArgumentNullException(nameof(filmPoint));
        _filmPointRepository.Update(filmPoint);
    }
}