using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Models;
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
    
    [HttpPost("login")]
    public async Task<ActionResult<LoginCredentialsDto>> Post(LoginCredentialsDto model)
    {
        await _userService.Login(model);
        return model;
    }
}