using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using webNET_Hits_backend_aspnet_project_2.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models.DTO;

public class OrderDto
{
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
}