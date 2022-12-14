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
[Route("api/order")]
[Produces("application/json")]
public class OrderController: ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ITokenService _tokenService;

    public OrderController(IOrderService orderService, ITokenService tokenService)
    {
        _orderService = orderService;
        _tokenService = tokenService;
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
        try
        {
            _tokenService.CheckAccessToken(Request.Headers[HeaderNames.Authorization].ToString()
                .Replace("Bearer ", ""));
            return Ok(await _orderService.GetInformationOrder(id, Guid.Parse(User.Identity!.Name!)));
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

    /// <summary>
    /// Get a list of orders
    /// </summary>
    [HttpGet]
    [Authorize]
    [ProducesResponseType(typeof(OrderInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public IActionResult GetListOrders()
    {
        try
        {
            _tokenService.CheckAccessToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
            var orders = _orderService.GetListOrders(Guid.Parse(User.Identity!.Name!));
            return Ok(orders);
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
        if (!ModelState.IsValid)
        {
            return BadRequest(new Response
            {
                Status = "Error",
                Message = "Model is incorrect"
            });
        }
        
        var idUser = Guid.Parse(User.Identity!.Name!);
        try
        {
            _tokenService.CheckAccessToken(Request.Headers[HeaderNames.Authorization].ToString()
                .Replace("Bearer ", ""));
            _orderService.CreatingOrderFromBasket(idUser, model);
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
        catch (BadHttpRequestException e)
        {
            return BadRequest(new Response
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
        try
        {
            _tokenService.CheckAccessToken(Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", ""));
            await _orderService.ConfirmOrderDelivery(id, Guid.Parse(User.Identity.Name));
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
        catch (BadHttpRequestException e)
        {
            return BadRequest(new Response
            {
                Status = "Error",
                Message = e.Message
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