using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;

namespace webNET_Hits_backend_aspnet_project_2.Models.DTO;

public class DishBasketDto
{
    public Guid Id { get; set; }
    
    [NotNull]
    [MinLength(1)]
    public string Name { get; set; }
    
    [NotNull]
    public double Price { get; set; }
    
    [NotNull]
    public double TotalPrice { get; set; }
    
    [NotNull]
    public int Amount { get; set; }

    [MaybeNull]
    public string? Image { get; set; }
}