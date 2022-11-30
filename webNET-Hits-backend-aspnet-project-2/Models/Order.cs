using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using webNET_Hits_backend_aspnet_project_2.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class Order
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    [DataType(DataType.DateTime)]
    [Required]
    public string DeliveryTime { get; set; }
    
    [DataType(DataType.DateTime)]
    [Required]
    public string OrderTime { get; set; }
    
    [Required]
    public OrderStatus Status { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    [MaybeNull]
    public List<DishBasket> Dishes { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Address { get; set; }
    
    public User User { get; set; }
}