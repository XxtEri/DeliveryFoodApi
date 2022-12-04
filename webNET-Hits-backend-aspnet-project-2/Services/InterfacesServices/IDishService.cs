using webNET_Hits_backend_aspnet_project_2.Enums;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

public interface IDishService
{
    Task<DishPagedListDto?> GetDishes(List<DishCategory> categories, bool vegetarian, SortingDish sorting, int page);
    DishDto GetInformationAboutDish(Guid id);
    void AddDishes();
}