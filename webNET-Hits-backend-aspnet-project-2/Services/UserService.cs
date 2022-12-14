using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webNET_Hits_backend_aspnet_project_2.JWT;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Servises;

public class UserService: IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TokenResponse> RegisterUser(UserRegisterModel model)
    {
        if (model.BirthDate != null && DateTime.Parse(model.BirthDate) >= DateTime.UtcNow)
        {
            throw new BadHttpRequestException( message: "Invalid birthdate. Birthdate must be more than current datetime");
        }
        
        var identity = await GetIdentity(model.Email, model.Password);

        if (identity != null)
        {
            throw new NullReferenceException(message: $"Username {model.Email} is already taken.");
        }
        
        await AddUser(model);
        
        var modelLog = new LoginCredentials
        {
            Email = model.Email,
            Password = model.Password
        };

        return await LogInUser(modelLog);
    }

    public async Task<TokenResponse> LogInUser(LoginCredentials model)
    {
        var identity = await GetIdentity(model.Email, model.Password);

        if (identity == null)
        {
            throw new NullReferenceException(message: "Login or password Failed");
        }

        return new TokenResponse
        {
            Token = GetEncodeJwtToken(identity)
        };
    }

    public async Task<UserDto> GetProfileUser(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        return new UserDto
        {
            Id = user.Id,
            FullName = user!.FullName,
            BirthDate = user.BirthDate!,
            Email = user.Email,
            Gender = user.Gender,
            Address = user.Address!,
            PhoneNumber = user.PhoneNumber!
        };
    }
    
    public async Task EditProfileUser(Guid userId, UserEditModel model)
    {
        if (model.BirthDate != null && DateTime.Parse(model.BirthDate) >= DateTime.UtcNow)
        {
            throw new BadHttpRequestException( message: "Invalid birthdate. Birthdate must be more than current datetime");
        }
        
        var users = _context.Users
            .Where(x => x.Id == userId)
            .AsEnumerable()
            .Select(x =>
                {
                    x.FullName = model.FullName;
                    x.BirthDate = model.BirthDate!;
                    x.Gender = model.Gender;
                    x.Address = model.Address!;
                    x.PhoneNumber = model.PhoneNumber!;
                    return x;
                });

        foreach (var user in users)
        {
            _context.Users.Entry(user).State = EntityState.Modified;
        }
        
        await _context.SaveChangesAsync();
    }

    public async Task LogOut(string token)
    {
        await _context.DisactiveTokens.AddAsync(new DisactiveToken
        {
            Token = token,
            TimeLogOut = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();
    }

    private string GetEncodeJwtToken(ClaimsIdentity? identity)
    {
        var now = DateTime.UtcNow;
        //создаем JWT токен
        var jwt = new JwtSecurityToken(
            issuer: JwtConfigurations.Issuer,
            audience: JwtConfigurations.Audience,
            notBefore: now,
            claims: identity?.Claims,
            expires: now.AddMinutes(JwtConfigurations.Lifetime),
            signingCredentials: new SigningCredentials(JwtConfigurations.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(jwt);
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

    private async Task AddUser(UserRegisterModel model)
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
    
}
