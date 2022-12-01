using webNET_Hits_backend_aspnet_project_2.Models;

namespace webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

public interface IUserService
{
    Task<TokenResponse> LogInUser(LoginCredentials model);
    Task AddUser(UserRegisterModel model);
}