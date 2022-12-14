using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Controllers;

[ApiController]
[Route("api/account")]
[Produces("application/json")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Register new user
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
    {
        try
        {
            var token = await _userService.RegisterUser(model);
            return Ok(token);
        }
        catch (NullReferenceException e)
        {
            return BadRequest(new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
    }
    
    /// <summary>
    /// Log in to the system
    /// </summary>
    /// /// <param name="request">log in request</param>
    /// <returns>jwt token</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginCredentials model)
    {
        try
        {
            var token = await _userService.LogInUser(model);

            return Ok(token);
        }
        catch (NullReferenceException e)
        {
            return BadRequest(new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
    }

    /// <summary>
    /// Log out system user
    /// </summary>
    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public string Logout()
    {
        return "ok";
        //добавить токен в базу данных действующих, но неиспользуемых токенов
    }

    /// <summary>
    /// Get user profile
    /// </summary>
    [HttpGet("profile")]
    [Authorize]
    [ProducesResponseType(typeof(UserEditModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserProfile()
    {
        try
        {
            await _userService.GetProfileUser(Guid.Parse(User.Identity!.Name!));
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(500, new Response
            {
                Status = "Error",
                Message = "Unknown error"
            });
        }
    }

    /// <summary>
    /// Edit user Profile
    /// </summary>
    [HttpPut("profile")]
    [Authorize]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditUserProfile([FromBody] UserEditModel model)
    {
        try
        {
            await _userService.EditProfileUser(Guid.Parse(User.Identity!.Name!), model);
            return Ok();
        }
        catch (Exception)
        {
            return StatusCode(500, new Response
            {
                Status = "Error",
                Message = "Unknown error"
            });
        }
    }
}