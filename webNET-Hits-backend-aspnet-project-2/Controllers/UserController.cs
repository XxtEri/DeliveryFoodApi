using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Controllers;

[ApiController]
[Route("api/account")]
[Produces("application/json")]
public class UserController: ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public UserController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
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
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Logout()
    {
        try
        {
            var token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            _tokenService.CheckAccessToken(token);
            await _userService.LogOut(token);
            return StatusCode(200, new Response
            {
                Status = null,
                Message = "Logged out"
            });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new Response
            {
                Status = "Error",
                Message = "User is not authorized"
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response
            {
                Status = "error",
                Message = e.Message
            });
        }
    }

    /// <summary>
    /// Get user profile
    /// </summary>
    [HttpGet("profile")]
    [Authorize]
    [ProducesResponseType(typeof(UserEditModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserProfile()
    {
        try
        {
            _tokenService.CheckAccessToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
            return Ok(await _userService.GetProfileUser(Guid.Parse(User.Identity!.Name!)));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new Response
            {
                Status = "Error",
                Message = "User is not authorized"
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response
            {
                Status = "Error",
                Message = e.Message
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
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> EditUserProfile([FromBody] UserEditModel model)
    {
        try
        {
            _tokenService.CheckAccessToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
            await _userService.EditProfileUser(Guid.Parse(User.Identity!.Name!), model);
            return Ok();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new Response
            {
                Status = "Error",
                Message = "User is not authorized"
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
    }
}