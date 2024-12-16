using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using Core;
using Core.DTO;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Repository.Data_Services;
using Repository.Domain;
using Repository.Repositories;

namespace SynopsisAPI.Controllers;

[ApiController]
[Route("Authorize")]
public class AuthorizeController(Context ctx, IConfiguration configuration) : ControllerBase
{
    private string UiUrl = $"http://localhost:5277/login-response?token=";
    [HttpGet("/login")]
    public async Task<IActionResult> Login(string email, string password)
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        using (var rep = new Repository<User>(ctx))
        {
            var ds = new UserService(rep);
            var user = ds.GetUserByEmail(email);
            if(user is null) return Problem("User not found or password is incorrect.");
            if(user.Password is not null && user.Password == password)
            {
                var token = GenerateJwtToken(user);
                return Redirect($"{UiUrl}{Uri.EscapeDataString(token)}");
            }
        }
        return Problem("User not found or password is incorrect.");
    }
    
    
    [HttpPost("/register")]
    public IActionResult Register(string email, string password, string name, string surname)
    {
        using (var rep = new Repository<User>(ctx))
        {
            var ds = new UserService(rep);
            if (ds.GetUserByEmail(email) != null)
            {
                return Problem("User already exists.");
            }
            var user = new User {Email = email, Password = password, FirstName = name, LastName = surname};
            ds.AddUser(user);
            var token =GenerateJwtToken(user);
            return Redirect($"{UiUrl}{Uri.EscapeDataString(token)}");
        }
    }
    
    [HttpGet("/discord/login")]
    public async Task<IActionResult> DiscordLogin()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var redirectUri = Url.Action("DiscordResponse", "Authorize", null, Request.Scheme);
        var discordOAuthUrl = $"https://discord.com/api/oauth2/authorize?client_id={Env.GetString("DISCORD_CLIENT_ID")}&redirect_uri={Uri.EscapeDataString(redirectUri)}&response_type=code&scope=identify%20email";
        return Redirect(discordOAuthUrl);
    }

    
    [HttpGet("/google/login")]
    public async Task<IActionResult> GoogleLogin()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("/google/response")]
    public async Task<IActionResult> GoogleResponse()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
        if (!authenticateResult.Succeeded)
        {
            return Unauthorized();
        }

        var claims = authenticateResult.Principal?.Identities.FirstOrDefault()?.Claims;
        var googleId = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        var avatarUrl = claims?.FirstOrDefault(c => c.Type == "picture")?.Value;
    
        if (googleId == null || email == null || name == null)
        {
            return Problem("Failed to retrieve Google user information.");
        }

        using (var rep = new Repository<User>(ctx))
        {
            var ds = new UserService(rep);
            var user = ds.GetUserByEmail(email);
            if (user == null)
            {
               
                user = new User{GoogleId = googleId, Email = email
                    , FirstName = name.Split(" ")[0], LastName = name.Split(" ")[1]??""
                    , AvatarUrl = avatarUrl};
                ds.AddUser(user);
            }else if (user.GoogleId is null)
            {
                user.GoogleId = googleId;
                ds.UpdateUser(user);
            }
            var token = GenerateJwtToken(user);
            return Redirect($"{UiUrl}{Uri.EscapeDataString(token)}");
        }
    }
    
    [HttpGet("/discord/response")]
    public async Task<IActionResult> DiscordResponse(string code)
{
    if (string.IsNullOrEmpty(code))
    {
        return Problem("Authorization code not provided.");
    }

    try
    {
        // Step 1: Exchange the authorization code for an access token
        var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "https://discord.com/api/oauth2/token")
        {
            Content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", Env.GetString("DISCORD_CLIENT_ID")),
                new KeyValuePair<string, string>("client_secret", Env.GetString("DISCORD_CLIENT_SECRET")),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", $"{Request.Scheme}://{Request.Host}/discord/response")
            })
        };

        var httpClient = new HttpClient();
        var tokenResponse = await httpClient.SendAsync(tokenRequest);
        if (!tokenResponse.IsSuccessStatusCode)
        {
            return Problem("Failed to retrieve access token from Discord.");
        }

        var tokenResponseContent = await tokenResponse.Content.ReadAsStringAsync();
        var tokenData = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenResponseContent);
        if (!tokenData.TryGetValue("access_token", out var accessToken))
        {
            return Problem("Access token not found in the response.");
        }

        // Step 2: Fetch the user's Discord profile
        var userRequest = new HttpRequestMessage(HttpMethod.Get, "https://discord.com/api/users/@me");
        userRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var userResponse = await httpClient.SendAsync(userRequest);
        if (!userResponse.IsSuccessStatusCode)
        {
            return Problem("Failed to fetch user information from Discord.");
        }

        var userContent = await userResponse.Content.ReadAsStringAsync();
        var discordUser = JsonConvert.DeserializeObject<DiscordUser>(userContent);

        using (var rep = new Repository<User>(ctx))
        {
            var ds = new UserService(rep);

            // Step 3: Check if the user exists in the database or create a new one
            var user = ds.GetUserByEmail(discordUser.Email);
            if (user == null)
            {
                user = new User
                {
                    DiscordId = discordUser.Id,
                    FirstName = discordUser.Username,
                    Email = discordUser.Email,
                    AvatarUrl = $"https://cdn.discordapp.com/avatars/{discordUser.Id}/{discordUser.Avatar}.png"
                };
                ds.AddUser(user);
            }
            else if (user.DiscordId == null)
            {
                user.DiscordId = discordUser.Id;
                ds.UpdateUser(user);
            }

            // Step 4: Generate and return a JWT token
            var token = GenerateJwtToken(user);
            return Redirect($"{UiUrl}{Uri.EscapeDataString(token)}");
        }
    }
    catch (Exception ex)
    {
        return Problem($"An error occurred: {ex.Message}");
    }
}
    
    
    
    private string GenerateJwtToken(User user)
    {
        Env.Load();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("JWT_KEY")));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email??string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role) 
        };

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(60),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}