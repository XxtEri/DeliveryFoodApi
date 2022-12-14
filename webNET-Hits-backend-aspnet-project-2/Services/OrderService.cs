using System.Data.Entity.Core;
using System.Runtime.InteropServices;
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

    public async Task<OrderDto> GetInformationOrder(Guid orderId, Guid userId)
    {
        var order = await _context.Orders.FindAsync(orderId);

        if (order == null)
        {
            throw new ObjectNotFoundException(message: $"The order with id={orderId} is not on found");
        }

        if (order.UserId != userId)
        {
            throw new ExternalException(message: $"User with id={userId} has insufficient rights");
        }

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

    public async Task CreatingOrderFromBasket(Guid userId, OrderCreateDto model)
    {
        if (DateTime.Parse(model.DeliveryTime) <= DateTime.UtcNow.AddHours(1))
        {
            throw new BadHttpRequestException( message: "Invalid delivery time. Delivery time must be more than current datetime on 60 minutes");
        }
        
        var dishes = _context.BasketDishes
            .Where(x => x.UserId == userId)
            .ToList();

        if (dishes.Count == 0)
        {
            throw new ObjectNotFoundException(message: $"Empty basket for user with id={userId}");
        }
        
        var order = new Order
        {
            UserId = userId,
            DeliveryTime = model.DeliveryTime,
            OrderTime = DateTime.Now.ToString(),
            Address = model.Address,
            Status = OrderStatus.InProcess,
            Price = SumPriceDishes(dishes)
        };
        
        _context.Orders.Add(order);

        ClearBasket(dishes);
        AddDishes(dishes, order.Id);
        
        await _context.SaveChangesAsync();
    }

    public async Task ConfirmOrderDelivery(Guid orderId, Guid userId)
    {
        var order = _context.Orders.Find(orderId);

        if (order == null)
        {
            throw new ObjectNotFoundException(message: $"Order with id={orderId} don't in database");
        }

        if (order.UserId != userId)
        {
            throw new Exception(message: $"User with id={userId} has insufficient rights");
        }
        
        if (order.Status != OrderStatus.InProcess)
        {
            throw new BadHttpRequestException(message: $"Can't update status for order with id={orderId}");
        }
        
        order.Status = OrderStatus.Delivered;
        _context.Orders.Entry(order).State = EntityState.Modified;

        await _context.SaveChangesAsync();
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