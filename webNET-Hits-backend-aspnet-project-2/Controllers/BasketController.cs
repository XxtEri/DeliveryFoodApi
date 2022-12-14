using System.Data.Entity.Core;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
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
    private readonly ITokenService _tokenService;

    public BasketController(IBasketService basketService, ITokenService tokenService)
    {
        _basketService = basketService;
        _tokenService = tokenService;
    }
    
    /// <summary>
    /// Get user cart
    /// </summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(DishBasketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public IActionResult GetDishesInBasket()
    {
        try
        {
            _tokenService.CheckAccessToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
            return Ok(_basketService.GetBasketDishes(Guid.Parse(User.Identity!.Name!)));
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new Response
            {
                Status = "Error",
                Message = "User is not authorized"
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
    }

    /// <summary>
    /// Add dish to cart
    /// </summary>
    [HttpPost("dish/{dishId}")]
    [Authorize]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AddDishToBasket(Guid dishId)
    {
        try
        {
            _tokenService.CheckAccessToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
            await _basketService.AddDishInBasket(Guid.Parse(User.Identity!.Name!), dishId);
            return Ok();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new Response
            {
                Status = "Error",
                Message = "User is not authorized"
            });
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
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
    public async Task<IActionResult> DeleteDishInBasket(Guid dishId, bool increase)
    {
        try
        {
            _tokenService.CheckAccessToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
            await _basketService.DeleteDishOfBasket(Guid.Parse(User.Identity!.Name!), dishId, increase);
            return Ok();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new Response
            {
                Status = "Error",
                Message = "User is not authorized"
            });
        }
        catch (ExternalException e)
        {
            return StatusCode(403, new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
    }
}