using webNET_Hits_backend_aspnet_project_2.Models.DTO;

namespace webNET_Hits_backend_aspnet_project_2.Servises.InterfacesServices;

public interface IOrderService
{
    Task<string> CheckErrors(Guid idOrder, Guid idUser);
    Task<OrderDto>  GetInformationOrder(Guid idOrder);
    OrderInfoDto[] GetListOrders(Guid idUser);
    string CreatingOrderFromBasket(Guid idUser, OrderCreateDto model);
    Task ConfirmOrderDelivery(Guid orderId, Guid userId);
}