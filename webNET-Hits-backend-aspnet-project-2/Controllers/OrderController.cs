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
        var idUser = Guid.Parse(User.Identity!.Name!);
        var response = await _orderService.CheckErrors(id, idUser);

        return response switch
        {
            "not found" => NotFound(new Response
            {
                Status = "Error",
                Message = $"Order with id={id} don't in database"
            }),
            "forbidden" => StatusCode(403, new Response
            {
                Status = "Error",
                Message = $"User with id={idUser} has insufficient rights"
            }),  
            "ok" => Ok(await _orderService.GetInformationOrder(id))
        };
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
        return Ok(_orderService.GetListOrders(Guid.Parse(User.Identity!.Name!)));
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
        var response = _orderService.CreatingOrderFromBasket(idUser, model);

        return response switch
        {
            "bad request" => BadRequest(new Response
            {
                Status = "Error",
                Message = $"Empty basket for user with id={idUser}"
            }),
            "ok" => Ok()
        };
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
        var response = await _orderService.ConfirmOrderDelivery(id, userId);
        
        return response switch
        {
            "not found" => NotFound(new Response
            {
                Status = "Error",
                Message = $"Order with id={id} don't in database"
            }),
            "forbidden" => StatusCode(403, new Response
            {
                Status = "Error",
                Message = $"User with id={userId} has insufficient rights"
            }),
            "bad request" => BadRequest(new Response
            {
                Status = "Error",
                Message = $"Can't update status for order with id={id}"
            }),
            
            "ok" => Ok()
        };
    }
}