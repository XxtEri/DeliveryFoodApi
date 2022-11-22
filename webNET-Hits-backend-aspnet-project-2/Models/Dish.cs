using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class Dish
{
    public enum DishCategory
    {
        Wok,
        Pizza,
        Soup,
        Dessert,
        Drink
    }
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public double Price { get; set; }
    
    public string Image { get; set; }
    
    public bool Vegetarian { get; set; }
    
    public double Rating { get; set; }
    
    public DishCategory Category { get; set; }
}