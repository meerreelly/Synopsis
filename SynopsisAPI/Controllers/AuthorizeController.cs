using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository.Data_Services;
using Repository.Domain;
using Repository.Repositories;

namespace SynopsisAPI.Controllers;

[ApiController]
[Route("Authorize")]
public class AuthorizeController(Context ctx, IConfiguration configuration) : ControllerBase
{
    [HttpGet("/login")]
    public IActionResult Login(string email, string password)
    {
        using (var rep = new Repository<User>(ctx))
        {
            var ds = new UserService(rep);
            var user = ds.GetUserByEmail(email);
            if(user is null) return Problem("User not found or password is incorrect.");
            if(user.Password is not null && user.Password == password)
            {
                var token = GenerateJwtToken(user);
                return Ok(token);
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
            return Ok(GenerateJwtToken(user));
        }
    }
    
    
    [HttpGet("/google/login")]
    public IActionResult GoogleLogin()
    {
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
            return Ok(token);
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