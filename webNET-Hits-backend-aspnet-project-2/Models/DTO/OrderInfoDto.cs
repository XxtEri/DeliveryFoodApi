using System.ComponentModel.DataAnnotations;
using webNET_Hits_backend_aspnet_project_2.Enums;

namespace webNET_Hits_backend_aspnet_project_2.Models.DTO;

public class OrderInfoDto
{
    public Guid Id { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public string DeliveryTime { get; set; }
    
    [Required]
    [DataType(DataType.DateTime)]
    public string OrderTime { get; set; }
    
    [Required]
    public OrderStatus Status { get; set; }
    
    [Required]
    public double Price { get; set; }
}