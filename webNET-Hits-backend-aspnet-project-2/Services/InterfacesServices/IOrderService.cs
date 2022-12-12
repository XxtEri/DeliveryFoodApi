using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

public interface IOrderService
{
    Task<OrderDto>  GetInformationOrder(Guid orderId, Guid userId);
    OrderInfoDto[] GetListOrders(Guid idUser);
    Task CreatingOrderFromBasket(Guid idUser, OrderCreateDto model);
    Task ConfirmOrderDelivery(Guid orderId, Guid userId);
}