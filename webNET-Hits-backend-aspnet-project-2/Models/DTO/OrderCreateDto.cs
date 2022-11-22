using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace webNET_Hits_backend_aspnet_project_2.Models.DTO;

public class OrderCreateDto
{
    [NotNull]
    public DateTime deliveryTime { get; set; }
    
    [NotNull]
    [MinLength(1)]
    public string Address { get; set; }
}