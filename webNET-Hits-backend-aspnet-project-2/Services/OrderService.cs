using webNET_Hits_backend_aspnet_project_2.Enums;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Servises;

public class OrderService: IOrderService
{
    private readonly ApplicationDbContext _context;

    public OrderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string> CheckErrors(Guid idOrder, Guid idUser)
    {
        var order = await _context.Orders.FindAsync(idOrder);

        if (order == null)
        {
            return "not found";
        }

        if (order.UserId != idUser)
        {
            return "forbidden";
        }

        return "ok";
    }

    public async Task<OrderDto> GetInformationOrder(Guid idOrder)
    {
        var order = await _context.Orders.FindAsync(idOrder);
        
        var view = new OrderDto
        {
            Id = order.Id,
            DeliveryTime = order.DeliveryTime,
            OrderTime = order.OrderTime,
            Status = order.Status,
            Price = order.Price,
            Address = order.Address
        };

        if (order.Dishes != null)
        {
            foreach (var dish in order.Dishes)
            {
                view.Dishes?.Add(new DishBasketDto
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    Price = dish.Price,
                    TotalPrice = dish.TotalPrice,
                    Amount = dish.Amount,
                    Image = dish.Image
                });
                await _context.SaveChangesAsync();
            }
        }
        
        return view;
    }

    public OrderInfoDto[] GetListOrders(Guid idUser)
    {
        return _context.Orders
            .Where(model => model.Id == idUser)
            .Select(model => new OrderInfoDto
            {
                Id = model.Id,
                DeliveryTime = model.DeliveryTime,
                OrderTime = model.OrderTime,
                Status = model.Status,
                Price = model.Price
            }).ToArray();
    }

    public async Task<string> CreatingOrderFromBasket(Guid idUser, OrderCreateDto model)
    {
        var dishes = _context.BasketDishes
            .Where(x => x.UserId == idUser)
            .ToList();
        
        if (dishes.Count == 0)
        {
            return "bad request";
        }

        await _context.Orders.AddAsync(new Order
        {
            UserId = idUser,
            DeliveryTime = model.DeliveryTime,
            OrderTime = DateTime.Now.ToString(),
            Address = model.Address,
            Status = OrderStatus.InProcess,
            Price = SumPriceDishes(dishes),
            Dishes = dishes
        });
        
        foreach (var dishBasket in dishes)
        {
            _context.BasketDishes.Remove(dishBasket);
        }
        
        await _context.SaveChangesAsync();
        return "ok";
    }
    
    private double SumPriceDishes(List<DishBasket> dishes)
    {
        double sum = 0;

        foreach (var dish in dishes)
        {
            sum += dish.TotalPrice;
        }

        return sum;
    }
}