using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace webNET_Hits_backend_aspnet_project_2.Models.DTO;

public enum OrderStatus
{
    InProcess,
    Delivered
}

public class OrderDto
{
    [Key]
    public Guid Id { get; set; }
    
    [NotNull]
    public DateTime DeliveryTime { get; set; }
    
    [NotNull]
    public DateTime OrderTime { get; set; }
    
    [NotNull]
    public OrderStatus Status { get; set; }
    
    [NotNull]
    public int Price { get; set; }
    
    [NotNull]
    public DishBasket dishes { get; set; }
    
    [NotNull]
    [MinLength(1)]
    public string Address { get; set; }
}