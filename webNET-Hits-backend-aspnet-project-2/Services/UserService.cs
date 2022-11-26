using System.Security.Claims;
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
    
    private readonly List<LoginCredentials> _users = new List<LoginCredentials>
    {
        new LoginCredentials { Email = "admin@example.com", Password = "pass"}
    };

    public async Task<ClaimsIdentity> GetIdentity(string email, string password)
    {
        var user = _users.FirstOrDefault(x => x.Email == email && x.Password == password);
        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };
    
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
    
            return claimsIdentity;
        }
    
        return null;
    }
}