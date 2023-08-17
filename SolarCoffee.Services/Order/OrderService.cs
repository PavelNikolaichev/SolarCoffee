using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql.Internal;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;

using SolarCoffee.Services.Inventory;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Services.Order;

public class OrderService : IOrderService
{
    private readonly SolarDbContext db;
    private readonly ILogger<OrderService> logger;
    private readonly IProductService productService;
    private readonly IInventoryService inventoryService;


    public OrderService(
        SolarDbContext db,
        ILogger<OrderService> logger, 
        IProductService productService, 
        IInventoryService inventoryService
        ) {
        this.db = db;
        this.logger = logger;
        this.productService = productService;
        this.inventoryService = inventoryService;
    }

    /// <summary>
    /// Create an open SalesOrder
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public ServiceResponse<bool> GenerateInvoice(SalesOrder order)
    {
        logger.LogInformation("Generating new order...");

        foreach (var item in order.SalesOrderItems)
        {
            item.Product = productService.GetProduct(item.Product.Id);
            item.Quantity = item.Quantity;

            var inventoryId = inventoryService.GetInventory(item.Product.Id).Id;

            inventoryService.UpdateUnitsAvailable(inventoryId, -item.Quantity);
        }

        try
        {
            db.SalesOrders.Add(order);
            db.SaveChanges();

            return new ServiceResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = "Open order created"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>
            {
                IsSuccess = false,
                Data = false,
                Message = ex.StackTrace
            };
        }
    }


    /// <summary>
    /// Returns a list of SalesOrders
    /// </summary>
    /// <returns></returns>
    public List<SalesOrder> GetAllOrders()
    {
        return db.SalesOrders
            .Include(so => so.Customer)
                .ThenInclude(customer => customer.PrimaryAddress)
            .Include(so => so.SalesOrderItems)
                .ThenInclude(item => item.Product)
            .ToList();
    }

    /// <summary>
    /// Mark open order SalesOrder as paid
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ServiceResponse<bool> MarkFulfilled(int id)
    {
        var order = db.SalesOrders.Find(id);

        order.UpdatedOn = DateTime.UtcNow;
        order.IsPaid = true;

        try
        {
            db.SalesOrders.Update(order);
            db.SaveChanges();

            return new ServiceResponse<bool>
            {
                IsSuccess = true,
                Data = true,
                Message = $"Order {order.Id} closed"
            };
        } 
        catch (Exception ex)
        {
            return new ServiceResponse<bool>
            {
                IsSuccess = false,
                Data = false,
                Message = ex.StackTrace
            };
        }
    }
}
