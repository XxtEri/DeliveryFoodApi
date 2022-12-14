using System.Data.Entity.Core;
using System.Runtime.InteropServices;
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
                    TotalPrice = x.TotalPrice,
                    Amount = x.Amount,
                    Image = x.Image
                }).ToArray();
    }

    public async Task AddDishInBasket(Guid userId, Guid dishId)
    {
        var dishBasket = await _context.BasketDishes.FindAsync(dishId);

        if (dishBasket != null && dishBasket.UserId == userId)
        {
            dishBasket.Amount += 1;
            await _context.SaveChangesAsync();
            
        }
        else
        {
            var model = await _context.Dishes.FindAsync(dishId);
        
            if (model == null)
            {
                throw new ObjectNotFoundException(message: $"The dish with id={dishId} is not on the menu");
            }
            
            var user = (await _context.Users.FindAsync(userId))!;

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
        }
    }

    public async Task DeleteDishOfBasket(Guid userId, Guid dishId, bool increase)
    {
        var dishBasket = await _context.BasketDishes.FindAsync(dishId);
        
        if (dishBasket == null)
        {
            throw new ObjectNotFoundException(message: $"The dish with id={dishId} is not on the menu");
        }
        
        if (dishBasket.UserId != userId)
        {
            throw new ExternalException();
        }

        if (increase && dishBasket.Amount > 1)
        {
            dishBasket.Amount -= 1;
            
        } else
        {
            _context.Remove(dishBasket);
        }

        await _context.SaveChangesAsync();
    }
}