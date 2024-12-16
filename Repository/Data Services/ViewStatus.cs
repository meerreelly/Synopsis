using Core;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data_Services;

public class ViewStatusService
{
    private readonly Repository<ViewStatus> _viewStatusRepository;

    public ViewStatusService(Repository<ViewStatus> viewStatusRepository)
    {
        _viewStatusRepository = viewStatusRepository;
    }

    public IEnumerable<ViewStatus> GetAllViewStatuses()
    {
        return _viewStatusRepository.GetAllQueryable()
            .Include(vs => vs.User)
            .Include(vs => vs.Film);
    }

    public ViewStatus GetViewStatusById(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));

        var viewStatus = _viewStatusRepository.GetAllQueryable()
            .Include(vs => vs.User)
            .Include(vs => vs.Film)
            .FirstOrDefault(vs => vs.ViewStatusId == id);
        if (viewStatus == null) throw new Exception("ViewStatus not found");

        return viewStatus;
    }
    
    public ViewStatus GetViewStatusByFilmAndUserId(int Filmid, int UserId)
    {
        var viewStatus = _viewStatusRepository.GetAllQueryable()
            .FirstOrDefault(vs => vs.FilmId == Filmid && vs.UserId == UserId);

        return viewStatus;
    }

    public IEnumerable<ViewStatus> GetViewStatusesByUserId(int userId)
    {
        if (userId < 0) throw new ArgumentNullException(nameof(userId));

        return _viewStatusRepository.GetAllQueryable()
            .Include(vs => vs.Film)
            .Where(vs => vs.UserId == userId)
            .ToList();
    }

    public IEnumerable<ViewStatus> GetViewStatusesByFilmId(int filmId)
    {
        if (filmId < 0) throw new ArgumentNullException(nameof(filmId));

        return _viewStatusRepository.GetAllQueryable()
            .Include(vs => vs.User)
            .Where(vs => vs.FilmId == filmId)
            .ToList();
    }

    public void AddViewStatus(ViewStatus viewStatus)
    {
        if (viewStatus == null) throw new ArgumentNullException(nameof(viewStatus));
        _viewStatusRepository.Add(viewStatus);
    }

    public void DeleteViewStatus(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));
        ViewStatus viewStatus = _viewStatusRepository.GetById(id);
        if (viewStatus == null) throw new Exception("ViewStatus not found");
        _viewStatusRepository.Delete(viewStatus);
    }

    public void UpdateViewStatus(ViewStatus viewStatus)
    {
        if (viewStatus == null) throw new ArgumentNullException(nameof(viewStatus));
        _viewStatusRepository.Update(viewStatus);
    }

    public void AddViewStatusRange(IEnumerable<ViewStatus> viewStatuses)
    {
        if (viewStatuses == null) throw new ArgumentNullException(nameof(viewStatuses));
        _viewStatusRepository.AddRange(viewStatuses);
    }
}
