using System.Diagnostics.CodeAnalysis;

namespace webNET_Hits_backend_aspnet_project_2.Models.DTO;

public class DishPagedListDto
{
    [MaybeNull]
    public Dish[]  Dishes { get; set; }
    
    public PageInfoModel Pagination { get; set; }
}