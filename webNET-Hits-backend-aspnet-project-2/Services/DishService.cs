using Microsoft.EntityFrameworkCore;
using webNET_Hits_backend_aspnet_project_2.Enums;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Servises;

public class DishService: IDishService
{
    private readonly ApplicationDbContext _context;

    public DishService(ApplicationDbContext context)
    {
        _context = context;
        if (!_context.Dishes.Any())
        {
            AddDishes();
        }
    }

    public async Task<DishPagedListDto> GetDishes(List<DishCategory> categories, bool vegetarian, SortingDish sorting, int page)
    {
        var dishes = GetListDishDto(categories, vegetarian);
        
        dishes = SortingDishes(dishes, sorting);

        var pageSize = 5;
        var countDishes = await dishes.CountAsync();
        var count = countDishes % pageSize < pageSize ? countDishes / 5 + 1 : countDishes / 5;

        if (page > count)
        {
            throw new BadHttpRequestException(message: "Invalid value for attribute page");
        }

        var items = dishes.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        
        return new DishPagedListDto()
        {
            Dishes = items,
            PageInfoModel = new PageInfoModel(pageSize, count, page)
        };
    }
    
    public DishDto GetInformationAboutDish(Guid dishId)
    {
        var dish = _context.Dishes.Find(dishId);

        if (dish == null)
        {
            throw new NullReferenceException(message: $"Dish with id = {dishId} don't in database");
        }
        
        return new DishDto
        {
            Name = dish.Name,
            Description = dish.Description,
            Price = dish.Price,
            Image = dish.Image,
            Vegetarian = dish.Vegetarian,
            Rating = GetRatingDish(dish.Rating),
            Category = dish.Category,
            Id = dish.Id
        };
    }
    
    public bool CheckSetRating(Guid userId, Guid dishId)
    {
        var dish = _context.OrderingDishes
            .Where(x => x.DishId == dishId && x.UserId == userId)
            .Select(x => x)
            .ToList();

        if (dish.Count == 0)
        {
            return false;
        }
        
        return true;
    }
    
    public async Task SetRating(Guid userId, Guid dishId, int ratingScore)
    {
        var dish = await _context.Dishes.FindAsync(dishId);
        
        dish.Rating.Add(ratingScore);
        _context.Dishes.Entry(dish).State = EntityState.Modified;
    }

    private static double GetRatingDish(IReadOnlyCollection<double>? ratingList)
    {
        if (ratingList == null || ratingList.Count == 0)
        {
            return 0;
        }
        
        return ratingList.Sum() / ratingList.Count;
    }

    private IQueryable<DishDto> GetListDishDto(List<DishCategory> categories, bool vegetarian)
    {
        bool isEmptyCategories = categories.Count == 0 ? true : false;
        
        return vegetarian switch
        {
            true => _context.Dishes.Where(x => x.Vegetarian == true && (isEmptyCategories || categories.Contains(x.Category))).Select(x => new DishDto
                    { 
                        Name = x.Name,
                        Description = x.Description, 
                        Price = x.Price,
                        Image = x.Image,
                        Vegetarian = x.Vegetarian,
                        Rating = GetRatingDish(x.Rating),
                        Category = x.Category,
                        Id = x.Id
                    }),
            false => _context.Dishes.Where(x => isEmptyCategories || categories.Contains(x.Category)).Select(x => new DishDto
                    { 
                        Name = x.Name,
                        Description = x.Description, 
                        Price = x.Price,
                        Image = x.Image,
                        Vegetarian = x.Vegetarian,
                        Rating = GetRatingDish(x.Rating),
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

    private async void AddDishes()
    {
        _context.Dishes.Add(new Dish
        {
            Name = "A4 сыра",
            Description = "4 сыра: «Моцарелла», «Гауда», «Фета», «Дор-блю», сливочно-сырный соус, пряные травы",
            Price = 360,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = true,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "Party BBQ",
            Description = "Бекон, соленый огурчик, брусника, сыр «Моцарелла», сыр «Гауда», соус BBQ",
            Price = 480,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = false,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "B сыра",
            Description = "4 сыра: «Моцарелла», «Гауда», «Фета», «Дор-блю», сливочно-сырный соус, пряные травы",
            Price = 360,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = true,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "Psdarty BBQ",
            Description = "Бекон, соленый огурчик, брусника, сыр «Моцарелла», сыр «Гауда», соус BBQ",
            Price = 480,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = false,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "4f сыра",
            Description = "4 сыра: «Моцарелла», «Гауда», «Фета», «Дор-блю», сливочно-сырный соус, пряные травы",
            Price = 360,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = true,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "Party BBQ",
            Description = "Бекон, соленый огурчик, брусника, сыр «Моцарелла», сыр «Гауда», соус BBQ",
            Price = 480,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = false,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "4a сыра",
            Description = "4 сыра: «Моцарелла», «Гауда», «Фета», «Дор-блю», сливочно-сырный соус, пряные травы",
            Price = 360,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = true,
            Category = DishCategory.Pizza
        });
        _context.Dishes.Add(new Dish
        {
            Name = "Party BBQ",
            Description = "Бекон, соленый огурчик, брусника, сыр «Моцарелла», сыр «Гауда», соус BBQ",
            Price = 480,
            Image = "https://mistertako.ru/uploads/products/77888c7e-8327-11ec-8575-0050569dbef0.",
            Vegetarian = false,
            Category = DishCategory.Pizza
        });

        await _context.SaveChangesAsync();
    }
}