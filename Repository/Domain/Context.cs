using Core;
using DotNetEnv;
using Microsoft.EntityFrameworkCore;

namespace Repository.Domain;

public class Context:DbContext
{
    public DbSet<FilmPoint> FilmPoints => Set<FilmPoint>();
    public DbSet<Film> Films => Set<Film>();
    public DbSet<TitleType> TitleTypes => Set<TitleType>();
    public DbSet<Actor> Actors => Set<Actor>();
    public DbSet<Director> Directors => Set<Director>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<FilmLink> FilmLinks => Set<FilmLink>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Overview> Overviews => Set<Overview>();
    public DbSet<ViewStatus> ViewStatuses => Set<ViewStatus>();
    
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string connectionString = Env.GetString("DATABASE");
        optionsBuilder.UseNpgsql(connectionString);
    }
    
}