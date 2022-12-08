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
        var userId = Guid.Parse(User.Identity!.Name!);
        var response = _basketService.AddDishInBasket(userId, dishId).Result;

        return response switch
        {
            "not found" => NotFound(new Response
            {
                Status = "Error",
                Message = $"The dish with id={dishId} is not on the menu"
            }),
            "forbidden" => StatusCode(403, new Response
            {
                Status = "Error",
                Message = $"User with id={userId} has insufficient rights"
            }),
            _ => Ok()
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
        var userId = Guid.Parse(User.Identity!.Name!);
        var response = _basketService.DeleteDishOfBasket(userId, dishId, increase).Result;

        return response switch
        {
            "not found" => NotFound(new Response
            {
                Status = "Error",
                Message = $"The dish with id={dishId} is not on the menu"
            }),
            "forbidden" => StatusCode(403, new Response
            {
                Status = "Error",
                Message = $"User with id={userId} has insufficient rights"
            }),
            _ => Ok()
        };
    }
}