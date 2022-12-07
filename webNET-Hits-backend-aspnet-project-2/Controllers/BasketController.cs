using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Controllers;

[ApiController]
[Route("api/basket")]
[Produces("application/json")]
public class BasketController: ControllerBase
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }
    
    /// <summary>
    /// Get user cart
    /// </summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(DishBasketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public IEnumerable<DishBasketDto> GetDishesInBasket()
    {
        return _basketService.GetBasketDishes(Guid.Parse(User.Identity!.Name!));
    }

    /// <summary>
    /// Add dish to cart
    /// </summary>
    [HttpPost("dish/{dishId}")]
    [Authorize]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public IActionResult AddDishToBasket(Guid dishId)
    {
        var response = _basketService.AddDishInBasket(Guid.Parse(User.Identity!.Name!), dishId).Result;

        return response switch
        {
            "ok" => Ok(),
            "not found" => NotFound(new Response
            {
                Status = "Error",
                Message = $"Dish with id={dishId} don't in basket"
            })
        };
    }

    /// <summary>
    /// Decrease the number of dishes in the cart (if increase = true), or remove the dish completely (increase = false)
    /// </summary>
    [HttpDelete("dish/{dishId}")]
    [Authorize]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public IActionResult DeleteDishInBasket(Guid dishId, bool increase)
    {
        var response = _basketService.DeleteDishOfBasket(Guid.Parse(User.Identity!.Name!), dishId, increase).Result;

        return response switch
        {
            "ok" => Ok(),
            "not found" => NotFound(new Response
            {
                Status = "Error",
                Message = $"Dish with id={dishId} don't in basket"
            })
        };
    }
}