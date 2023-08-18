using Microsoft.AspNetCore.Mvc;
using SolarCoffee.Services.Customer;
using SolarCoffee.Services.Order;
using SolarCoffee.Web.Serializers;
using SolarCoffee.Web.ViewModels;
using System.Security.Cryptography.X509Certificates;

namespace SolarCoffee.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> logger;
    private readonly IOrderService orderService;
    private readonly ICustomerService customerService;

    public OrderController(
        ILogger<OrderController> logger,
        IOrderService orderService,
        ICustomerService customerService
        ) {
        this.logger = logger;
        this.orderService = orderService;
        this.customerService = customerService;
    }

    [HttpPost("/api/invoice")]
    public ActionResult GenerateNewOrder([FromBody] InvoiceModel invoice)
    {
        logger.LogInformation("Generating Invoice...");

        var order = OrderMapper.SerializeInvoiceToOrder(invoice);
        order.Customer = customerService.GetCustomer(invoice.CustomerId);
        orderService.GenerateInvoice(order);
        return Ok();
    }
}
