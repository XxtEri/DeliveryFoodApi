using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Models;

namespace webNET_Hits_backend_aspnet_project_2.Controllers;

[ApiController]
[Route("api/dish")]
public class DishController
{
    private ApplicationDbContext _context;

    public DishController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public String GetListDishes(bool vegetarian, int page)
    {
        return page.ToString();
    }
    
}