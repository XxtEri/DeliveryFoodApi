using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Servises;

public class TokenService: ITokenService
{
    private readonly ApplicationDbContext _context;

    public TokenService(ApplicationDbContext context)
    {
        _context = context;
    }

    public void CheckAccessToken(string token)
    {
        if (_context.DisactiveTokens.FirstOrDefault(x => x.Token == token) != null)
        {
            throw new UnauthorizedAccessException();
        }
    }
}