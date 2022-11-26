using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

public interface IDishService
{
    public Task GenerateTMP();
    public DishDto[] GenerateDishes();
    public Task Add(DishDto model);
}