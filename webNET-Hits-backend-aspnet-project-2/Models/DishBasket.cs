using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class DishBasket
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Name { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    [Required]
    public double TotalPrice { get; set; }
    
    [Required]
    public int Amount { get; set; }
    
    [MaybeNull]
    public string Image { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    
    [Required]
    public User User { get; set; }
}