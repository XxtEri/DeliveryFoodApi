using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Servises;

public class BasketService
{
    private readonly ApplicationDbContext _context;

    public BasketService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task GetBasketDishes()
    {
        var tmpModel = new DishBasketDto()
        {
            Amount = Random.Shared.Next(1, 1000),
            Name = "Name",
            Price = Random.Shared.Next(1, 100),
            TotalPrice = Random.Shared.Next(1, 1000)
        };

        await Add(tmpModel);
    }

    private async Task Add(DishBasketDto model)
    {
        await _context.DishBaskets.AddAsync(new DishBasket
        {
            Amount = model.Amount,
            Name = model.Name,
            Price = model.Price,
            TotalPrice = model.TotalPrice
        });

        await _context.SaveChangesAsync();
    }
}