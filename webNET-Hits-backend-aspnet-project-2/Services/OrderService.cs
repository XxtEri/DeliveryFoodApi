using Microsoft.AspNetCore.Mvc;
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

    public async Task<ActionResult<OrderDto>> GetInformationOrder(Guid idOrder)
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
            }
        }
        
        return new OkObjectResult(view);
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
}