using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using webNET_Hits_backend_aspnet_project_2.JWT;
using webNET_Hits_backend_aspnet_project_2.Models;
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
    public async Task<IActionResult> Register([FromBody] UserRegisterModel model)
    {
        await _userService.AddUser(model);
        
        var now = DateTime.UtcNow;
        //создаем JWT токен
        var jwt = new JwtSecurityToken(
            issuer: JwtConfigurations.Issuer,
            audience: JwtConfigurations.Audience,
            notBefore: now,
            expires: now.AddMinutes(JwtConfigurations.Lifetime),
            signingCredentials: new SigningCredentials(JwtConfigurations.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            token = encodeJwt
        };

        return new JsonResult(response);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCredentials model)
    {
        var identity = await _userService.GetIdentity(model.Email, model.Password);

        var now = DateTime.UtcNow;
        //создаем JWT токен
        var jwt = new JwtSecurityToken(
            issuer: JwtConfigurations.Issuer,
            audience: JwtConfigurations.Audience,
            notBefore: now,
            claims: identity.Claims,
            expires: now.AddMinutes(JwtConfigurations.Lifetime),
            signingCredentials: new SigningCredentials(JwtConfigurations.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            token = encodeJwt
        };

        return new JsonResult(response);
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