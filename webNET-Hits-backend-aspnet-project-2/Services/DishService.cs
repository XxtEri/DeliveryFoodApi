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
    
    private Dish[] _tmp;

    public DishService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task GenerateTMP()
    {
        var tmpModel = new DishDto
        {
            Name = Dishes[Random.Shared.Next(Dishes.Length)],
            Price = Random.Shared.Next(10, 1000),
            Image =
                "https://www.google.com/url?sa=i&url=https%3A%2F%2Frecepty.7dach.ru%2Flublu_gotovit%2Fchto-prigotovit-na-vtoroe-10-receptov-v-pomosch-hozyayke-167444.html&psig=AOvVaw1DNOnqzH8dXNfVkpdgLmG5&ust=1669462653478000&source=images&cd=vfe&ved=0CBAQjRxqFwoTCPCJqpWfyfsCFQAAAAAdAAAAABAE",
            Vegetarian = false,
            Rating = Random.Shared.Next(1, 10),
            Category = DishCategory.Dessert
        };

        await Add(tmpModel);
        return;
    }

    public DishDto[] GenerateDishes()
    {
        return _context.Dishes.Select(x => new DishDto
        {
            Name = x.Name,
            Price = x.Price,
            Image = x.Image,
            Vegetarian = x.Vegetarian,
            Rating = x.Rating,
            Category = x.Category
        }).ToArray();
    }

    public async Task Add(DishDto model)
    {
        await _context.Dishes.AddAsync(new Dish
        {
            Name = model.Name,
            Price = model.Price,
            Image = model.Image,
            Vegetarian = model.Vegetarian,
            Rating = model.Rating,
            Category = model.Category
        });

        await _context.SaveChangesAsync();
    }
}