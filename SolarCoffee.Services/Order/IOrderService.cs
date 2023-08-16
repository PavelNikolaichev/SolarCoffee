namespace SolarCoffee.Services.Order;

public interface IOrderService
{
    List<Data.Models.SalesOrder> GetAllOrders();

    ServiceResponse<bool> GenerateInvoice(Data.Models.SalesOrder order);

    ServiceResponse<bool> MarkFulfilled(int id);
}
