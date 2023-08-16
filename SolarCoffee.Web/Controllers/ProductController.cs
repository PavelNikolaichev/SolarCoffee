using Microsoft.AspNetCore.Mvc;
using SolarCoffee.Services.Product;
using SolarCoffee.Web.Serializers;

namespace SolarCoffee.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> logger;
    private readonly ICustomerService productService;

    public ProductController(ILogger<ProductController> logger, ICustomerService productService)
    {
        this.logger = logger;
        this.productService = productService;
    }

    [HttpGet("/api/product")]
    public ActionResult GetProduct()
    {
        logger.LogInformation("Getting all products...");

        var products = productService.GetAllProducts()
            .Select(product => ProductMapper.SerializeProductModel(product));

        return Ok(products);
    }
}
