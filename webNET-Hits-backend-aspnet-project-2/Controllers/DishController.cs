using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Enums;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;
using webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

namespace webNET_Hits_backend_aspnet_project_2.Controllers;

[ApiController]
[Route("api/dish")]
public class DishController
{
    private readonly IDishService _dishService;

    public DishController(IDishService dishService)
    {
        _dishService = dishService;
    }

    [HttpGet]
    public IEnumerable<DishDto> GetListDishes([FromQuery] List<DishCategory> categories, bool vegetarian, SortingDish sorting, int page)
    {
        return _dishService.GenerateDishes();
    }

    [HttpGet("{id}")]
    public string GetInformationConcreteDish(Guid id)
    {
        return id.ToString();
    }


    [HttpGet("{id}/rating/check")]
    public string CheckCurrentUserSetRating(int id)
    {
        return id.ToString();
    }

    [HttpPost("{id}/rating")]
    public String SetRatingOfDish(int id, int ratingScore)
    {
        return "Ok";
    }
}