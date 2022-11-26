using System.Diagnostics.CodeAnalysis;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Models;

public class DishPagedList
{
    [MaybeNull]
    public DishDto[]  Dishes { get; set; }
    
    public PageInfoModel Pagination { get; set; }
}