using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace webNET_Hits_backend_aspnet_project_2.Models.DTO;

public enum DishCategory
{
    Wok,
    Pizza,
    Soup,
    Dessert,
    Drink
}

public class DishDto
{
    public Guid Id { get; set; }
    
    [NotNull]
    [MinLength(1)]
    public string Name { get; set; }
    
    [MaybeNull]
    public string Description { get; set; }
    
    [NotNull]
    public double Price { get; set; }
    
    [MaybeNull]
    public string Image { get; set; }
    
    public bool Vegetarian { get; set; }
    
    [MaybeNull]
    public double Rating { get; set; }
    
    public DishCategory Category { get; set; }
}