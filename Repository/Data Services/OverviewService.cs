using Core;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data_Services;

public class OverviewService
{
    private readonly Repository<Overview> _overviewRepository;

    public OverviewService(Repository<Overview> overviewRepository)
    {
        _overviewRepository = overviewRepository;
    }

    public IEnumerable<Overview> GetAllOverviews()
    {
        return _overviewRepository.GetAllQueryable()
            .Include(o => o.Film)
            .Include(o => o.User);
    }

    public Overview GetOverviewById(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));

        var overview = _overviewRepository.GetAllQueryable()
            .Include(o => o.Film)
            .Include(o => o.User)
            .FirstOrDefault(o => o.OverviewId == id);
        if (overview == null) throw new Exception("Overview not found");

        return overview;
    }

    public void AddOverview(Overview overview)
    {
        if (overview == null) throw new ArgumentNullException(nameof(overview));
        _overviewRepository.Add(overview);
    }

    public void DeleteOverview(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));
        Overview overview = _overviewRepository.GetById(id);
        if (overview == null) throw new Exception("Overview not found");
        _overviewRepository.Delete(overview);
    }

    public void AddOverviewRange(IEnumerable<Overview> overviews)
    {
        if (overviews == null) throw new ArgumentNullException(nameof(overviews));
        _overviewRepository.AddRange(overviews);
    }

    public void UpdateOverview(Overview overview)
    {
        if (overview == null) throw new ArgumentNullException(nameof(overview));
        _overviewRepository.Update(overview);
    }
}