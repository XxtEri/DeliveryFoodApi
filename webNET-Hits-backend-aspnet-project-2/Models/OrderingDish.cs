using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class OrderingDish
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey("Order")]
    public Guid OrderId { get; set; }
    
    [Required]
    public Guid DishId { get; set; }
    
    [Required]
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
    
    public Order Order { get; set; }
}