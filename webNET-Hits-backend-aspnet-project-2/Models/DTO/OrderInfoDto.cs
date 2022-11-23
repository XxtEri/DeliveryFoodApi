using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace webNET_Hits_backend_aspnet_project_2.Models.DTO;

public class OrderInfoDto
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
}