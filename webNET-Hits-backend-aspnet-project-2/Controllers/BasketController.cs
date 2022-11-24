using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using webNET_Hits_backend_aspnet_project_2.Models;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Controllers;

[ApiController]
[Route("api/")]

public class BasketController: ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BasketController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet("basket")]
    public String Get()
    {
        return "Bla";
    }

    // [HttpPost]
    // public Task<IActionResult> Post(DishBasketDto model)
    // {
    //     return Ok();
    // }
}