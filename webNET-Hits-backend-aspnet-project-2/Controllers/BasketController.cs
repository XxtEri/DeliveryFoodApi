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
    private readonly ApplicationDbContext _context;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }
    
    /// <summary>
    /// Get user cart
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(DishBasketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public IEnumerable<DishBasketDto> GetDishesInBasket()
    {
        Guid idUser = Guid.Parse(User.Identity!.Name!);
        
        return _basketService.GetBasketDishes(idUser);
    }

    /// <summary>
    /// Add dish to cart
    /// </summary>
    [HttpPost("dish/{dishId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public IActionResult AddDishToBasket(Guid dishId)
    {

        return Ok(_basketService.AddDishInBasket(dishId));
    }

    /// <summary>
    /// Decrease the number of dishes in the cart (if increase = true), ot remove the dish completely (increase = false)
    /// </summary>
    [HttpDelete("dish/{dishId}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public String DeleteDishInBasket(Guid dishId, bool increase)
    {
        return "Ok";
    }
}