using webNET_Hits_backend_aspnet_project_2.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class Dish
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public double Price { get; set; }
    
    public string Image { get; set; }
    
    public bool Vegetarian { get; set; }
    
    public double Rating { get; set; }
    
    public DishCategory Category { get; set; }
}