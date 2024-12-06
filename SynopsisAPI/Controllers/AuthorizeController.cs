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
            var user = ds.GetUserByGoogleId(googleId);
            if (user == null)
            {
               
                user = new User{GoogleId = googleId, Email = email
                    , FirstName = name.Split(" ")[0], LastName = name.Split(" ")[1]??""
                    , AvatarUrl = avatarUrl};
                ds.AddUser(user);
            }
            var token = GenerateJwtToken(user);
            return Ok(token);//change
        }
    }
    
    private string GenerateJwtToken(User user)
    {
        Env.Load();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Env.GetString("JWT_KEY")));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.GoogleId),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("id", user.UserId.ToString()), 
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