using webNET_Hits_backend_aspnet_project_2.Enums;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

public interface IDishService
{
    List<DishDto> GetDishes(List<DishCategory> categories, bool vegetarian, SortingDish sorting);
    DishDto GetInformationAboutDish(Guid id);
    void AddDishes();
}