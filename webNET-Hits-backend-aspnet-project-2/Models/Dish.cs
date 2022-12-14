using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using webNET_Hits_backend_aspnet_project_2.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class Dish
{
    [Key]
    public Guid Id { get; set; }
    
    [MinLength(1)]
    [Required]
    public string Name { get; set; }
    
    public string? Description { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    [Url]
    [MaybeNull]
    public string Image { get; set; }
    
    public bool Vegetarian { get; set; }
    
    public double? Rating { get; set; }
    
    public DishCategory Category { get; set; }
}