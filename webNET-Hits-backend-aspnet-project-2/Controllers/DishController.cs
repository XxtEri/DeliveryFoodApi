using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Enums;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Controllers;

[ApiController]
[Route("api/dish")]
[Produces("application/json")]
public class DishController: Controller
{
    private readonly IDishService _dishService;

    public DishController(IDishService dishService)
    {
        _dishService = dishService;
    }

    /// <summary>
    /// Get a list of dishes (menu)
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(DishPagedListDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetListDishes([FromQuery] List<DishCategory> categories, [DefaultValue(false)] bool vegetarian, SortingDish sorting, [DefaultValue(1)] int page)
    {
        try
        {
            var viewModel = await _dishService.GetDishes(categories, vegetarian, sorting, page);
            return Ok(viewModel);
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
            return StatusCode(500, new Response
            {
                Status = "Error",
                Message = e.Message
            });
        }
    }

    /// <summary>
    /// Get information about concrete dish
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(DishDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public IActionResult GetInformationConcreteDish(Guid id)
    {
        try
        {
            var dish = _dishService.GetInformationAboutDish(id);
            return Ok(dish);
        }
        catch (NullReferenceException e)
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
    /// Check if user is able to set ration of the dish
    /// </summary>
    [HttpGet("{id}/rating/check")]
    [Authorize]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public IActionResult CheckCurrentUserSetRating(Guid id)
    {
        try
        {
            return Ok(_dishService.CheckSetRating(Guid.Parse(User.Identity!.Name!), id));
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
    /// Set a rating for a dish
    /// </summary>
    [HttpPost("{id}/rating")]
    [Authorize]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(void), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
    public IActionResult SetRatingOfDish(Guid id, int ratingScore)
    {
        try
        {
            _dishService.SetRating(Guid.Parse(User.Identity!.Name!), id, ratingScore);
            return Ok();
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