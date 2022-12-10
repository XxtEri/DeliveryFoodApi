using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

public interface IBasketService
{
    DishBasketDto[] GetBasketDishes(Guid id);
    Task AddDishInBasket(Guid idUser, Guid idDish);
    Task DeleteDishOfBasket(Guid idUser, Guid idDish, bool increase);
}