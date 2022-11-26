using System.Security.Claims;
using webNET_Hits_backend_aspnet_project_2.Models;

namespace webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

public interface IUserService
{
    Task<ClaimsIdentity> GetIdentity(string email, string password);
    Task AddUser(UserRegisterModel model);
}