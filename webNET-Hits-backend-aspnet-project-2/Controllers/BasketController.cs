using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Models;

namespace webNET_Hits_backend_aspnet_project_2.Controllers;

[ApiController]
[Route("api/basket")]

public class BasketController: ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BasketController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public String GetDishesInBasket()
    {
        return "Bla";
    }

    [HttpPost("dish/{dishId}")]
    public String AddDishToBasket(Guid dishId)
    {
        return "Ok";
    }

    [HttpDelete("dish/{dishId}")]
    public String DeleteDishInBasket(Guid dishId, bool increase)
    {
        return "Ok";
    }
}