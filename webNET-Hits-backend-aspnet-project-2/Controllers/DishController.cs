using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Enums;
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
    public String GetListDishes([FromQuery] List<DishCategory> categories, bool vegetarian, SortingDish sorting, int page)
    {
        return page.ToString();
    }

    [HttpGet("{id}")]
    public String GetInformationConcreteDish(Guid id)
    {
        return id.ToString();
    }


    [HttpGet("{id}/rating/check")]
    public String CheckCurrentUserSetRating(int id)
    {
        return id.ToString();
    }

    [HttpPost("{id}/rating")]
    public String SetRatingOfDish(int id, int ratingScore)
    {
        return "Ok";
    }
}