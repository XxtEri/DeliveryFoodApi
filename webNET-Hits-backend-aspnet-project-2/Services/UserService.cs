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

    public async Task<ClaimsIdentity> GetIdentity(string email, string password)
    {
        var user = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == password);
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
}