using System.ComponentModel.DataAnnotations;

namespace webNET_Hits_backend_aspnet_project_2.Models.DTO;

public class OrderCreate
{
    [Required]
    [DataType(DataType.DateTime)]
    public string DeliveryTime { get; set; }
    
    [Required]
    [MinLength(1)]
    public string Address { get; set; }
}