using Microsoft.AspNetCore.Mvc;
using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

public interface IOrderService
{
    Task<string> CheckErrors(Guid idOrder, Guid idUser);
    Task<ActionResult<OrderDto>>  GetInformationOrder(Guid idOrder);
    OrderInfoDto[] GetListOrders(Guid idUser);
}