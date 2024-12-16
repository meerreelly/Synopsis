using Newtonsoft.Json;

namespace Core.DTO;

public class DiscordTokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
    [JsonProperty("token_type")]
    public string TokenType { get; set; }
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }
    [JsonProperty("refresh_token")]
    public string RefreshToken { get; set; }
    [JsonProperty("scope")]
    public string Scope { get; set; }
}


public class DiscordUser
{
    [JsonProperty("id")]
    public string Id { get; set; }
    [JsonProperty("username")]
    public string Username { get; set; }
    [JsonProperty("avatar")]
    public string Avatar { get; set; }
    public string Email { get; set; }
}
