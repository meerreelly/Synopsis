using System.Security.Claims;
using Core;
using Core.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Repository.Data_Services;
using Repository.Domain;
using Repository.Repositories;

namespace SynopsisAPI.Controllers;
[ApiController]
[Route("UserSystem")]
public class UserController(Context ctx, IConfiguration configuration): ControllerBase
{
    [Authorize]
    [HttpGet("/getUserShort")]
    public IActionResult GetUserShort()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized("User ID not found in token");
        }
        
        using (var rep = new Repository<User>(ctx))
        {
            var ds = new UserService(rep);
            var user = ds.GetUserById(int.Parse(userId));
            return Ok(new UserShortDto(user));
        }
    }
    
    [Authorize]
    [HttpGet("/get-view-status/{filmId:int}")]
    public IActionResult GetViewStatus( int filmId)
    {
        using (var rep = new Repository<ViewStatus>(ctx))
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var ds = new ViewStatusService(rep);
            var status = ds.GetViewStatusByFilmAndUserId(filmId,int.Parse(userId));
            return Ok(status);
        }
    }
    [Authorize]
    [HttpPost("/set-view-status")]
    public IActionResult AddOrUpdateViewStatus([FromBody] ViewStatus viewStatus)
    {
        try
        {
            if (viewStatus == null)
                return BadRequest("ViewStatus is required.");
            
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            viewStatus.UserId = int.Parse(userId);

            using (var rep = new Repository<ViewStatus>(ctx))
            {
                var ds = new ViewStatusService(rep);
                var status = ds.GetViewStatusByFilmAndUserId(viewStatus.FilmId, viewStatus.UserId);
                if(status != null)
                {
                    status.Status = viewStatus.Status;
                    ds.UpdateViewStatus(status);
                }
                else
                {
                    ds.AddViewStatus(viewStatus);
                }
                return Ok(status);
            }
            
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [Authorize]
    [HttpGet("/get-user-data")]
    public IActionResult GetUserData()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return Unauthorized("User ID not found in token");
        }
        
        using (var rep = new Repository<User>(ctx))
        {
            var ds = new UserService(rep);
            var user = ds.GetUserById(int.Parse(userId));
            return Ok(new UserInfoDto(user));
        }
    }
    
    
    
}