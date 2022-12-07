using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class DishBasket
{
    [Key]
    [ForeignKey("Dish")]
    public Guid Id { get; set; }
    
    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    [Required]
    public double TotalPrice => Amount * Price;
    
    [Required]
    public int Amount { get; set; }
    
    [MaybeNull]
    public string Image { get; set; }
    
    [Required]
    public User User { get; set; }
    
    public Dish Dish { get; set; }
}