using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using webNET_Hits_backend_aspnet_project_2.Enums;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class Order
{
    [Key]
    public Guid Id { get; set; }
    
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
    public DishBasketDto Dishes { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Address { get; set; }
    
    [ForeignKey("User")]
    public int UserId { get; set; }
    
    [Required]
    public User User { get; set; }
}