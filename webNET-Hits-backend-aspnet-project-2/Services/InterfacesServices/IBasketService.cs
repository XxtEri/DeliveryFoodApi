using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

public interface IBasketService
{
    DishBasketDto[] GetBasketDishes(Guid id);
    Task<string> AddDishInBasket(Guid idUser, Guid idDish);
    Task<string> DeleteDishOfBasket(Guid idUser, Guid idDish, bool increase);
}