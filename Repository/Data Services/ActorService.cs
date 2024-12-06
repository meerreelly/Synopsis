using Core;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Data_Services;

public class ActorService
{
    private readonly Repository<Actor> _actorRepository;

    public ActorService(Repository<Actor> actorRepository)
    {
        _actorRepository = actorRepository;
    }

    public IEnumerable<Actor> GetAllActors()
    {
        return _actorRepository.GetAllQueryable()
            .Include(a => a.Films);
    }

    public Actor GetActorById(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));

        var actor = _actorRepository.GetAllQueryable()
            .Include(a => a.Films)
            .FirstOrDefault(a => a.ActorId == id);
        if (actor == null) throw new Exception("Actor not found");

        return actor;
    }

    public void AddActor(Actor actor)
    {
        if (actor == null) throw new ArgumentNullException(nameof(actor));
        _actorRepository.Add(actor);
    }

    public void DeleteActor(int id)
    {
        if (id < 0) throw new ArgumentNullException(nameof(id));
        Actor actor = _actorRepository.GetById(id);
        if (actor == null) throw new Exception("Actor not found");
        _actorRepository.Delete(actor);
    }

    public void AddActorRange(IEnumerable<Actor> actors)
    {
        if (actors == null) throw new ArgumentNullException(nameof(actors));
        _actorRepository.AddRange(actors);
    }

    public void UpdateActor(Actor actor)
    {
        if (actor == null) throw new ArgumentNullException(nameof(actor));
        _actorRepository.Update(actor);
    }
}