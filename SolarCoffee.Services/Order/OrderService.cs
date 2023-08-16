using SolarCoffee.Data;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Order;

public class OrderService : IOrderService
{
    private readonly SolarDbContext db;

    public OrderService(SolarDbContext db)
    {
        this.db = db;
    }

    public ServiceResponse<bool> GenerateInvoice(SalesOrder order)
    {
        throw new NotImplementedException();
    }

    public List<SalesOrder> GetAllOrders()
    {
        throw new NotImplementedException();
    }

    public ServiceResponse<bool> MarkFulfilled(int id)
    {
        throw new NotImplementedException();
    }
}
