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

    public DishBasketDto[] GetBasketDishes(Guid idUser)
    {
        return _context.BasketDishes
            .Where(x => x.UserId == idUser)
            .Select(x => new DishBasketDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Amount = x.Amount,
                    Image = x.Image
                }).ToArray();
    }

    public async Task<string> AddDishInBasket(Guid idUser, Guid idDish)
    {
        var dishBasket = await _context.BasketDishes.FindAsync(idDish);

        if (dishBasket != null && dishBasket.UserId == idUser)
        {
            dishBasket.Amount += 1;
            await _context.SaveChangesAsync();
            return "ok";
        }
        
        var model = await _context.Dishes.FindAsync(idDish);
        if (model == null)
        {
            return "not found";
        }
            
        var user = (await _context.Users.FindAsync(idUser))!;

        await _context.BasketDishes.AddAsync(new DishBasket
        {
            Id = model.Id,
            UserId = user.Id,
            Name = model!.Name!,
            Price = model.Price,
            Amount = 1,
            Image = model.Image!,
            User = user,
            Dish = model
        });
            
        await _context.SaveChangesAsync();

        return "ok";
        
        
    }

    public async Task<string> DeleteDishOfBasket(Guid idUser, Guid idDish, bool increase)
    {
        var dishBasket = await _context.BasketDishes.FindAsync(idDish);
        if (dishBasket == null)
        {
            return "not found";
        }

        if (increase && dishBasket.Amount > 1)
        {
            dishBasket.Amount -= 1;
        } else
        {
            _context.Remove(dishBasket);
        }

        await _context.SaveChangesAsync();
        return "ok";
    }
}