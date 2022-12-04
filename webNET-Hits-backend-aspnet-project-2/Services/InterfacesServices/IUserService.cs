using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

public interface IUserService
{
    Task<TokenResponse?> RegisterUser(UserRegisterModel model);
    Task<TokenResponse> LogInUser(LoginCredentials model);
    Task<UserDto> GetProfileUser(Guid id);
    Task EditProfileUser(Guid id, UserEditModel model);
}