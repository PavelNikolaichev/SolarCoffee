using Microsoft.AspNetCore.Mvc;
using SolarCoffee.Services.Customer;
using SolarCoffee.Web.Serializers;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ILogger<CustomerController> logger;
    private readonly ICustomerService customerService;

    public CustomerController(
        ILogger<CustomerController> logger,
        ICustomerService customerService
        ) {
        this.logger = logger;
        this.customerService = customerService;
    }

    [HttpPost("/api/customer")]
    public ActionResult CreateCustomer([FromBody] CustomerModel customer)
    {
        logger.LogInformation("Creating a customer...");

        customer.CreatedOn = DateTime.UtcNow;
        customer.UpdatedOn = DateTime.UtcNow;

        var newCustomer = customerService.CreateCustomer(CustomerMapper.SerializeCustomer(customer));

        return Ok(newCustomer);
    }

    [HttpGet("/api/customer")]
    public ActionResult GetCustomers()
    {
        logger.LogInformation("Getting customers...");
        var customers = customerService.GetAllCustomers();

        var customerModels = customers.Select(customer => new CustomerModel
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PrimaryAddress = CustomerMapper.MapCustomerAddress(customer.PrimaryAddress),
            CreatedOn = customer.CreatedOn,
            UpdatedOn = customer.UpdatedOn,
        })
        .OrderByDescending(customer => customer.CreatedOn)
        .ToList();

        return Ok(customerModels);
    }

    [HttpDelete("/api/customer/{id}")]
    public ActionResult DeleteCustomer(int id)
    {
        logger.LogInformation($"Deleting customer with id={id}...");
        var customers = customerService.GetAllCustomers();

        var response = customerService.DeleteCustomer(id);

        return Ok(response);
    }
}
