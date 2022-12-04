using Microsoft.EntityFrameworkCore;
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

    public List<DishDto> GetDishes(List<DishCategory> categories, bool vegetarian, SortingDish sorting)
    {
        var dishes = GetListDishDto(categories, vegetarian);
        
        dishes = SortingDishes(dishes, sorting);
        
        return dishes.AsNoTracking().ToList();
    }

    private IQueryable<DishDto> GetListDishDto(List<DishCategory> categories, bool vegetarian)
    {
        return vegetarian switch
        {
            true => _context.Dishes.Where(x => x.Vegetarian == true && categories.Contains(x.Category)).Select(x => new DishDto
                    { 
                        Name = x.Name,
                        Description = x.Description, 
                        Price = x.Price,
                        Image = x.Image,
                        Vegetarian = x.Vegetarian,
                        Rating = x.Rating,
                        Category = x.Category,
                        Id = x.Id
                    }),
            false => _context.Dishes.Where(x => categories.Contains(x.Category)).Select(x => new DishDto
                    { 
                        Name = x.Name,
                        Description = x.Description, 
                        Price = x.Price,
                        Image = x.Image,
                        Vegetarian = x.Vegetarian,
                        Rating = x.Rating,
                        Category = x.Category,
                        Id = x.Id
                    })
        };
    }

    private IQueryable<DishDto> SortingDishes(IQueryable<DishDto> dishes, SortingDish sorting)
    {
        return sorting switch
        {
            SortingDish.NameDesk => dishes.OrderByDescending(s => s.Name),
            SortingDish.PriceAsk => dishes.OrderBy(s => s.Price),
            SortingDish.PriceDesk => dishes.OrderByDescending(s => s.Price),
            SortingDish.RatingAsk => dishes.OrderBy(s => s.Rating),
            SortingDish.RatingDesk => dishes.OrderByDescending(s => s.Rating),
            _ => dishes.OrderBy(s => s.Name)
        };
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
            Name = "A4 сыра",
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
            Name = "B сыра",
            Description = "4 сыра: «Моцарелла», «Гауда», «Фета», «Дор-блю», сливочно-сырный соус, пряные травы",
            Price = 360,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = true,
            Rating = 3.5,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "Psdarty BBQ",
            Description = "Бекон, соленый огурчик, брусника, сыр «Моцарелла», сыр «Гауда», соус BBQ",
            Price = 480,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = false,
            Rating = null,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "4f сыра",
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
            Name = "4a сыра",
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