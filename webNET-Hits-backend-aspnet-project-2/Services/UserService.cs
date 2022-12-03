using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webNET_Hits_backend_aspnet_project_2.JWT;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Servises;

public class UserService: IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TokenResponse> LogInUser(LoginCredentials model)
    {
        var identity = await GetIdentity(model.Email, model.Password);

        if (identity == null)
        {
            return new TokenResponse
            {
                Token = null
            };
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

        var token = new TokenResponse
        {
            Token = encodeJwt
        };

        return token;
    }

    private async Task<ClaimsIdentity?> GetIdentity(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
        
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString())
            };
    
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
    
            return claimsIdentity;
        }
    
        return null;
    }

    public async Task AddUser(UserRegisterModel model)
    {
        await _context.Users.AddAsync(new User
        {
            FullName = model.FullName,
            Password = model.Password,
            Email = model.Email,
            Address = model.Address,
            BirthDate = model.BirthDate,
            Gender = model.Gender,
            PhoneNumber = model.PhoneNumber
        });

        await _context.SaveChangesAsync();
    }
    
    public async Task EditProfileUser(UserEditModel model)
    {
        var newModel = new User()
        {
            FullName = model.FullName,
            BirthDate = model.BirthDate,
            Gender = model.Gender,
            Address = model.Address,
            PhoneNumber = model.PhoneNumber
        };
        
        _context.Users.Entry(newModel).State = EntityState.Modified;
        await _context.SaveChangesAsync();

    }
}
