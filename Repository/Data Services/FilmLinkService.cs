using Core;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data_Services;

public class FilmLinkService
{
    private readonly Repository<FilmLink> _filmLinkRepository;

    public FilmLinkService(Repository<FilmLink> filmLinkRepository)
    {
        _filmLinkRepository = filmLinkRepository;
    }

    public IEnumerable<FilmLink> GetAllFilmLinks()
    {
        return _filmLinkRepository.GetAllQueryable()
            .Include(fl => fl.Film);
    }

    public FilmLink GetFilmLinkById(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));

        var filmLink = _filmLinkRepository.GetAllQueryable()
            .Include(fl => fl.Film)
            .FirstOrDefault(fl => fl.FilmLinkId == id);
        if (filmLink == null) throw new Exception("FilmLink not found");

        return filmLink;
    }

    public void AddFilmLink(FilmLink filmLink)
    {
        if (filmLink == null) throw new ArgumentNullException(nameof(filmLink));
        _filmLinkRepository.Add(filmLink);
    }

    public void DeleteFilmLink(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));
        FilmLink filmLink = _filmLinkRepository.GetById(id);
        if (filmLink == null) throw new Exception("FilmLink not found");
        _filmLinkRepository.Delete(filmLink);
    }

    public void AddFilmLinkRange(IEnumerable<FilmLink> filmLinks)
    {
        if (filmLinks == null) throw new ArgumentNullException(nameof(filmLinks));
        _filmLinkRepository.AddRange(filmLinks);
    }

    public void UpdateFilmLink(FilmLink filmLink)
    {
        if (filmLink == null) throw new ArgumentNullException(nameof(filmLink));
        _filmLinkRepository.Update(filmLink);
    }
}