using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Models;

namespace webNET_Hits_backend_aspnet_project_2.Controllers;

[ApiController]
[Route("api/order")]
public class OrderController
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public string GetInformationOrder(Guid id)
    {
        return id.ToString();
    }

    [HttpGet]
    public string GetListOrders()
    {
        return "Bla";
    }

    [HttpPost]
    public string CreateOrder()
    {
        return "Ok";
    }

    [HttpPost("/{id}/status")]
    public string ConfirmOrderDelivery(int id)
    {
        return "Ok";
    }
    
}