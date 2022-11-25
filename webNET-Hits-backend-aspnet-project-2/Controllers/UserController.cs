using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Controllers;

[ApiController]
[Route("api/account")]
public class UserController
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public string Register()
    {
        return "Ok";
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<LoginCredentialsDto>> Login(LoginCredentialsDto model)
    {
        await _userService.Login(model);
        return model;
    }

    [HttpPost("logout")]
    public string Logout()
    {
        return "Ok";
    }

    [HttpGet("profile")]
    public string GetUserProfile()
    {
        return "user";
    }

    [HttpPut("profile")]
    public string EditUserProfile()
    {
        return "user";
    }
}