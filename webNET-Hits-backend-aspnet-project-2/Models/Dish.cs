using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class Dish
{
    [Key]
    public Guid Id { get; set; }
    
    [MinLength(1)]
    [Required]
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    public string? Image { get; set; }
    
    public bool Vegetarian { get; set; }
    
    public List<double> Rating { get; set; }
    
    public DishCategory Category { get; set; }

    public Dish()
    {
        Rating = new List<double>();
    }
}