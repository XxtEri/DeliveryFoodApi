using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace webNET_Hits_backend_aspnet_project_2.Models.DTO;

public class DishBasketDto
{
    public Guid Id { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
    
    [Required]
    public double Price { get; set; }

    [Required] public double TotalPrice => Amount * Price;
    
    [Required]
    public int Amount { get; set; }
    
    [MaybeNull]
    public string Image { get; set; }
}