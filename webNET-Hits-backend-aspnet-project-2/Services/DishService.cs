using webNET_Hits_backend_aspnet_project_2.Enums;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Servises;

public class DishService: IDishService
{
    private readonly ApplicationDbContext _context;

    private static readonly string[] Dishes = new[]
    {
        "Potato", "Chicken", "Fish"
    };

    public DishService(ApplicationDbContext context)
    {
        _context = context;
        if (!_context.Dishes.Any())
        {
            AddDishes();
        }
    }

    public DishDto[] GenerateDishes()
    {
        return _context.Dishes.Select(x => new DishDto
        {
            Name = x.Name,
            Description = x.Description,
            Price = x.Price,
            Image = x.Image,
            Vegetarian = x.Vegetarian,
            Rating = x.Rating,
            Category = x.Category,
            Id = x.Id
        }).ToArray();
    }

    public DishDto GetInformationAboutDish(Guid id)
    {
        var dish = _context.Dishes.Find(id);
        return new DishDto
        {
            Name = dish.Name,
            Description = dish.Description,
            Price = dish.Price,
            Image = dish.Image,
            Vegetarian = dish.Vegetarian,
            Rating = dish.Rating,
            Category = dish.Category,
            Id = dish.Id
        };
    }

    public async void AddDishes()
    {
        _context.Dishes.Add(new Dish
        {
            Name = "4 сыра",
            Description = "4 сыра: «Моцарелла», «Гауда», «Фета», «Дор-блю», сливочно-сырный соус, пряные травы",
            Price = 360,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = true,
            Rating = 3.5,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "Party BBQ",
            Description = "Бекон, соленый огурчик, брусника, сыр «Моцарелла», сыр «Гауда», соус BBQ",
            Price = 480,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = false,
            Rating = null,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "4 сыра",
            Description = "4 сыра: «Моцарелла», «Гауда», «Фета», «Дор-блю», сливочно-сырный соус, пряные травы",
            Price = 360,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = true,
            Rating = 3.5,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "Party BBQ",
            Description = "Бекон, соленый огурчик, брусника, сыр «Моцарелла», сыр «Гауда», соус BBQ",
            Price = 480,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = false,
            Rating = null,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "4 сыра",
            Description = "4 сыра: «Моцарелла», «Гауда», «Фета», «Дор-блю», сливочно-сырный соус, пряные травы",
            Price = 360,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = true,
            Rating = 3.5,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "Party BBQ",
            Description = "Бекон, соленый огурчик, брусника, сыр «Моцарелла», сыр «Гауда», соус BBQ",
            Price = 480,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = false,
            Rating = null,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "4 сыра",
            Description = "4 сыра: «Моцарелла», «Гауда», «Фета», «Дор-блю», сливочно-сырный соус, пряные травы",
            Price = 360,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = true,
            Rating = 3.5,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "Party BBQ",
            Description = "Бекон, соленый огурчик, брусника, сыр «Моцарелла», сыр «Гауда», соус BBQ",
            Price = 480,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = false,
            Rating = null,
            Category = DishCategory.Pizza
        });

        await _context.SaveChangesAsync();
    }
}