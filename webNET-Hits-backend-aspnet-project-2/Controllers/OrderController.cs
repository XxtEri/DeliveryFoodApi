using System.Data.Entity.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Controllers;

[ApiController]
[Route("api/order")]
[Produces("application/json")]
public class OrderController: ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    /// <summary>
    /// Get information about concrete order
    /// </summary>
    [HttpGet("{id}")]
    [Authorize]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetInformationOrder(Guid id)
    {
        var userId = Guid.Parse(User.Identity!.Name!);

        try
        {
            var order = await _orderService.GetInformationOrder(id, userId);
            return Ok(order);
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
            return StatusCode(403, new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
        catch
        {
            return StatusCode(500, new Response
            {
                Status = "Error",
                Message = "Unknown error"
            });
        }
    }

    /// <summary>
    /// Get a list of orders
    /// </summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(OrderInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public IActionResult GetListOrders()
    {
        try
        {
            var orders = _orderService.GetListOrders(Guid.Parse(User.Identity!.Name!));
            return Ok(orders);
        }
        catch
        {
            return StatusCode(500, new Response
            {
                Status = "Error",
                Message = "Unknown error"
            });
        }
    }

    /// <summary>
    /// Creating the order from dishes in basket
    /// </summary>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public IActionResult CreateOrder([FromBody] OrderCreateDto model)
    {
        var idUser = Guid.Parse(User.Identity!.Name!);
        try
        {
            _orderService.CreatingOrderFromBasket(idUser, model);
            return Ok();
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
        catch
        {
            return StatusCode(500, new Response
            {
                Status = "Error",
                Message = "Unknown error"
            });
        }
    }

    /// <summary>
    /// Confirm order delivery
    /// </summary>
    [HttpPost("{id}/status")]
    [Authorize]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ConfirmOrderDelivery(Guid id)
    {
        var userId = Guid.Parse(User.Identity.Name);

        try
        {
            await _orderService.ConfirmOrderDelivery(id, userId);
            return Ok();
        }
        catch (ObjectNotFoundException e)
        {
            return NotFound(new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
        catch (BadHttpRequestException e)
        {
            return BadRequest(new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
        catch (Exception e)
        {
            return StatusCode(403, new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
        catch
        {
            return StatusCode(500, new Response
            {
                Status = "Error",
                Message = "Unknown error"
            });
        }
    }
}