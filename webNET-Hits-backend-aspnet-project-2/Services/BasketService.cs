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

    public async Task AddDishInBasket(Guid idUser, Guid idDish)
    {
        var model = _context.Dishes.Find(idDish);
        var dishBasket = _context.BasketDishes.Find(idDish);

        if (dishBasket != null && dishBasket.UserId == idUser)
        {
            dishBasket.Amount += 1;
        } else
        {
            await _context.BasketDishes.AddAsync(new DishBasket
            {
                Id = idDish,
                Name = model!.Name!,
                Price = model.Price,
                Amount = 1,
                Image = model.Image!,
                User = _context.Users.Find(idUser)!
            });
            
            await _context.SaveChangesAsync();
        }
    }

    public void DeleteDishOfBasket(Guid id)
    {
        
    }
}