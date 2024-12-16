using Core;
using Core.DTO;

namespace Synopsis_UI.ApiServices;

public class FilmService
{
    private readonly HttpClient _httpClient;

    public FilmService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<int> GetFilmCount()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<int>("getFilmCount");
        }
        catch(Exception e)
        {
            Console.WriteLine(e.Message);
            return 0;
        }
    }
    public async Task<IEnumerable<Film>> GetFilms(int page, int count, FilmSearchDto? searchDto)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync($"getRange?page={page}&count={count}", searchDto); 
               

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Film>>() ?? new List<Film>();
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
                return new List<Film>();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return new List<Film>();
        }
    }

    public async Task<Film> GetFilm(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Film>($"getFilm?id={id}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
}