using System.Net.Http.Headers;
using Core;
using Core.DTO;

namespace Synopsis_UI.ApiServices;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<UserShortDto> GetUserShortInfo(string token)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "getUserShort");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserShortDto>();
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode} {response.Headers}");
            }
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"HTTP error occurred: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

        return null; 
    }
    public async Task<ViewStatus> GetViewStatus(int filmId, string token)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"get-view-status/{filmId}");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ViewStatus>();
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"HTTP error occurred: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

        return null; 
    }
    
    public async Task<ViewStatus> AddOrUpdateViewStatus(ViewStatus viewStatus, string token)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "set-view-status")
            {
                Content = JsonContent.Create(viewStatus)
            };

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ViewStatus>();
            }
            else
            {
                Console.WriteLine($"Request failed with status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException httpEx)
        {
            Console.WriteLine($"HTTP error occurred: {httpEx.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }

        return null; 
    }
}