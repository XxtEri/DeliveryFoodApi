using Microsoft.EntityFrameworkCore;
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

        var dishes = _context.OrderingDishes
            .Where(x => x.OrderId == order.Id && x.UserId == order.UserId)
            .Select(x => new DishBasketDto
            {
                Id = x.DishId,
                Name = x.Name,
                Price = x.Price,
                TotalPrice = x.TotalPrice,
                Amount = x.Amount,
                Image = x.Image
            }).ToList();

        var orderView = new OrderDto
        {
            Id = order.Id,
            DeliveryTime = order.DeliveryTime,
            OrderTime = order.OrderTime,
            Status = order.Status,
            Price = order.Price,
            Address = order.Address,
            Dishes = dishes
        };

        return orderView;
    }

    public OrderInfoDto[] GetListOrders(Guid idUser)
    {
        return _context.Orders
            .Where(model => model.UserId == idUser)
            .Select(model => new OrderInfoDto
            {
                Id = model.Id,
                DeliveryTime = model.DeliveryTime,
                OrderTime = model.OrderTime,
                Status = model.Status,
                Price = model.Price
            }).ToArray();
    }

    public string CreatingOrderFromBasket(Guid idUser, OrderCreateDto model)
    {
        var dishes = _context.BasketDishes
            .Where(x => x.UserId == idUser)
            .ToList();

        if (dishes.Count == 0)
        {
            return "bad request";
        }

        var order = new Order
        {
            UserId = idUser,
            DeliveryTime = model.DeliveryTime,
            OrderTime = DateTime.Now.ToString(),
            Address = model.Address,
            Status = OrderStatus.InProcess,
            Price = SumPriceDishes(dishes)
        };
        
        _context.Orders.Add(order);
        _context.SaveChanges();
        
        ClearBasket(dishes);
        
        AddDishes(dishes, order.Id);

        return "ok";
    }

    public async Task ConfirmOrderDelivery(Guid orderId, Guid userId)
    {
        var order = _context.Orders.Find(orderId);

        order.Status = OrderStatus.Delivered;
        _context.Orders.Entry(order).State = EntityState.Modified;

        _context.SaveChanges();
    }

    private void AddDishes(List<DishBasket> dishes, Guid orderId)
    {
        foreach (var dish in dishes)
        {
            _context.OrderingDishes.Add(new OrderingDish
            {
                OrderId = orderId,
                DishId = dish.Id,
                UserId = dish.UserId,
                Name = dish.Name,
                Price = dish.Price,
                Amount = dish.Amount,
                Image = dish.Image
            });
            
            _context.SaveChanges();
        }
    }

    private void ClearBasket(List<DishBasket> dishes)
    {
        foreach (var dishBasket in dishes)
        {
            _context.BasketDishes.Remove(dishBasket);
        }
        _context.SaveChanges();
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