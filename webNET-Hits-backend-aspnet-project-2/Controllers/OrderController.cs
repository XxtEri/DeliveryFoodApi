using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Controllers;

[ApiController]
[Route("api/order")]
[Produces("application/json")]
public class OrderController
{
    private readonly ApplicationDbContext _context;

    public OrderController(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get information about concrete order
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public string GetInformationOrder(Guid id)
    {
        return id.ToString();
    }

    /// <summary>
    /// Get a list of orders
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(OrderInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public string GetListOrders()
    {
        return "Bla";
    }

    /// <summary>
    /// Creating the order from dishes in basket
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public string CreateOrder()
    {
        return "Ok";
    }

    /// <summary>
    /// Confirm order delivery
    /// </summary>
    [HttpPost("/{id}/status")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public string ConfirmOrderDelivery(int id)
    {
        return "Ok";
    }
    
}