using Repository.Domain;

class Program
{
    static void Main(string[] args)
    {
        var csvFilePath = "C:\\Users\\merely\\Desktop\\imdb_top_1000.csv";

        using var dbContext = new Context();
        var parser = new CsvFilmParser(dbContext);

        var films = parser.ParseCsv(csvFilePath);

        foreach (var film in films)
        {
            Console.WriteLine($"Title: {film.Title}, Rating: {film.ImdbRating}");
        }
    }
}