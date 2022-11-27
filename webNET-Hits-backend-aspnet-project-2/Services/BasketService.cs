using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Servises;

public class BasketService: IBasketService
{
    private readonly ApplicationDbContext _context;

    public BasketService(ApplicationDbContext context)
    {
        _context = context;
    }

    public DishBasketDto[] GetBasketDishes()
    {
        return _context.DishBaskets.Select(x => new DishBasketDto
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            Amount = x.Amount,
            Image = x.Image
        }).ToArray();
    }

    public async Task<DishBasketDto> AddDishInBasket(Guid id)
    {
        var model = _context.Dishes.Find(id);
        var dishBasket = _context.DishBaskets.Find(id);
        
        if (dishBasket!= null)
        {
            dishBasket.Amount += 1;
        } else
        {
            await _context.DishBaskets.AddAsync(new DishBasket
            {
                Id = id,
                Name = model.Name,
                Price = model.Price,
                Amount = 1,
                Image = model.Image
            });
            
            await _context.SaveChangesAsync();
        }

        var dishBasketDto = new DishBasketDto
        {
            Id = id,
            Name = model.Name,
            Price = model.Price,
            Amount = dishBasket.Amount,
            Image = model.Image
        };
        
        return dishBasketDto;
    }
}