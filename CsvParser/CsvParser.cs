using System.Globalization;
using Core;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using CsvParser;
using Microsoft.EntityFrameworkCore;
using Repository.Domain;

public class CsvFilmParser
{
    private readonly Context _dbContext;

    public CsvFilmParser(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Film> ParseCsv(string filePath)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        // Настройки CSV
        csv.Context.RegisterClassMap<FilmMap>();

        var records = csv.GetRecords<FilmRaw>().ToList();
        var films = new List<Film>();

        foreach (var rawFilm in records)
        {
            // Уникнення дублювання об'єктів
            var actors = GetOrCreateActors(new[] { rawFilm.Star1, rawFilm.Star2, rawFilm.Star3, rawFilm.Star4 });
            var directors = GetOrCreateDirectors(new[] { rawFilm.Director });
            var genres = GetOrCreateGenres(rawFilm.Genre);

            var film = new Film
            {
                PosterUrl = rawFilm.PosterUrl,
                Title = rawFilm.Title,
                TitleType = GetOrCreateTitleType("Film"),
                ReleasedYear = rawFilm.ReleasedYear,
                Certificate = rawFilm.Certificate,
                Runtime = ParseRuntime(rawFilm.Runtime),
                ImdbRating = rawFilm.ImdbRating,
                ShortStory = rawFilm.Overview,
                MetaScore = rawFilm.MetaScore ?? 0,
                NumberOfVotes = rawFilm.NumberOfVotes,
                Gross = ParseGross(rawFilm.Gross),
                Actors = actors,
                Directors = directors,
                Genres = genres
            };

            films.Add(film);
        }

        // Зберігаємо всі фільми в БД
        _dbContext.Films.AddRange(films);
        _dbContext.SaveChanges();

        return films;
    }

    private int ParseRuntime(string runtime)
    {
        if (runtime.Contains("min"))
        {
            return int.Parse(runtime.Replace("min", "").Trim());
        }
        return 0;
    }

    private double ParseGross(string gross)
    {
        if (string.IsNullOrWhiteSpace(gross)) return 0;
        return double.Parse(gross.Replace(",", ""), CultureInfo.InvariantCulture);
    }

    private ICollection<Actor> GetOrCreateActors(string[] actorNames)
    {
        var actors = new List<Actor>();

        foreach (var actorName in actorNames)
        {
            if (string.IsNullOrWhiteSpace(actorName)) continue;

            // Розділяємо ім'я на частини
            var nameParts = actorName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var firstName = nameParts.FirstOrDefault() ?? string.Empty;
            var lastName = nameParts.Skip(1).FirstOrDefault() ?? string.Empty;

            // Перевіряємо в базі
            var existingActor = _dbContext.Actors
                .FirstOrDefault(a => a.FirstName.ToLower().Trim() == firstName.ToLower().Trim() &&
                                     a.LastName.ToLower().Trim() == lastName.ToLower().Trim());

            if (existingActor != null)
            {
                actors.Add(existingActor);
            }
            else
            {
                // Додаємо нового актора
                var newActor = new Actor
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                _dbContext.Actors.Add(newActor);
                actors.Add(newActor);
            }
        }

        // Зберігаємо зміни до бази для нових акторів
        _dbContext.SaveChanges();
        return actors;
    }


    private ICollection<Director> GetOrCreateDirectors(string[] directorNames)
    {
        var directors = new List<Director>();

        foreach (var directorName in directorNames)
        {
            if (string.IsNullOrWhiteSpace(directorName)) continue;

            // Розділяємо ім'я на частини
            var nameParts = directorName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var firstName = nameParts.FirstOrDefault() ?? string.Empty;
            var lastName = nameParts.Skip(1).FirstOrDefault() ?? string.Empty;

            // Завантажуємо всі записи в пам'ять для порівняння (ефективно для невеликих наборів даних)
            var existingDirector = _dbContext.Directors
                .FirstOrDefault(d => d.FirstName.ToLower() == firstName.ToLower() &&
                                     d.LastName.ToLower() == lastName.ToLower());

            if (existingDirector != null)
            {
                directors.Add(existingDirector);
            }
            else
            {
                // Додаємо нового режисера
                var newDirector = new Director
                {
                    FirstName = firstName,
                    LastName = lastName
                };

                _dbContext.Directors.Add(newDirector);
                directors.Add(newDirector);
            }
        }

        // Зберігаємо зміни до бази для нових режисерів
        _dbContext.SaveChanges();
        return directors;
    }


    private ICollection<Genre> GetOrCreateGenres(string genreString)
    {
        var genres = new List<Genre>();
        var genreNames = genreString.Split(',').Select(g => g.Trim());

        foreach (var genreName in genreNames)
        {
            if (string.IsNullOrWhiteSpace(genreName)) continue;

            var existingGenre = _dbContext.Genres.FirstOrDefault(g => g.Name == genreName);

            if (existingGenre != null)
            {
                genres.Add(existingGenre);
            }
            else
            {
                var newGenre = new Genre
                {
                    Name = genreName
                };

                _dbContext.Genres.Add(newGenre);
                genres.Add(newGenre);
            }
        }

        _dbContext.SaveChanges();
        return genres;
    }

    private TitleType GetOrCreateTitleType(string typeName)
    {
        var existingType = _dbContext.TitleTypes.FirstOrDefault(t => t.Name == typeName);

        if (existingType != null)
        {
            return existingType;
        }
        else
        {
            var newType = new TitleType
            {
                Name = typeName
            };

            _dbContext.TitleTypes.Add(newType);
            _dbContext.SaveChanges();
            return newType;
        }
    }
}
