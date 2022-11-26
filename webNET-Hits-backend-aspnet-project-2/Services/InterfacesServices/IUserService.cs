using System.Security.Claims;

namespace webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

public interface IUserService
{
    Task<ClaimsIdentity> GetIdentity(string email, string password);
}