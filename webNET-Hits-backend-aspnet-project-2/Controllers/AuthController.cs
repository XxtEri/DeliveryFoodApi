using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using webNET_Hits_backend_aspnet_project_2.JWT;
using webNET_Hits_backend_aspnet_project_2.Models;

namespace webNET_Hits_backend_aspnet_project_2.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController: ControllerBase
{
    private readonly List<LoginCredentials> _users = new List<LoginCredentials>
    {
        new LoginCredentials { Email = "admin@example.com", Password = "pass"}
    };

    [HttpPost("login")]
    public async Task<IActionResult> Token([FromBody] LoginCredentials model)
    {
        var identity = await GetIdentity(model.Email, model.Password);
        if (identity == null)
        {
            return BadRequest(new { errorText = "Invalid username or password" });
        }

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
    
    [HttpPost("register")]
    public async Task<IActionResult> Token([FromBody] UserRegisterModel model)
    {
        //TODO: добавить данные пользователя в таблицу User
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
    

    private async Task<ClaimsIdentity> GetIdentity(string email, string password)
    {
        var user = _users.FirstOrDefault(x => x.Email == email && x.Password == password);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };

            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }

        return null;
    }
}