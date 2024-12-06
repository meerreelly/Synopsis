using Core;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data_Services;

public class DirectorService
{
    private readonly Repository<Director> _directorRepository;

    public DirectorService(Repository<Director> directorRepository)
    {
        _directorRepository = directorRepository;
    }

    public IEnumerable<Director> GetAllDirectors()
    {
        return _directorRepository.GetAllQueryable()
            .Include(d => d.Films);
    }

    public Director GetDirectorById(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));

        var director = _directorRepository.GetAllQueryable()
            .Include(d => d.Films)
            .FirstOrDefault(d => d.DirectorId == id);
        if (director == null) throw new Exception("Director not found");

        return director;
    }

    public void AddDirector(Director director)
    {
        if (director == null) throw new ArgumentNullException(nameof(director));
        _directorRepository.Add(director);
    }

    public void DeleteDirector(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));
        Director director = _directorRepository.GetById(id);
        if (director == null) throw new Exception("Director not found");
        _directorRepository.Delete(director);
    }

    public void AddDirectorRange(IEnumerable<Director> directors)
    {
        if (directors == null) throw new ArgumentNullException(nameof(directors));
        _directorRepository.AddRange(directors);
    }

    public void UpdateDirector(Director director)
    {
        if (director == null) throw new ArgumentNullException(nameof(director));
        _directorRepository.Update(director);
    }
}