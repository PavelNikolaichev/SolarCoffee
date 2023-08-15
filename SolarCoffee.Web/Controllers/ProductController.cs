using Microsoft.AspNetCore.Mvc;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> logger;
    private readonly IProductService productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        this.logger = logger;
        this.productService = productService;
    }

    [HttpGet("/api/product")]
    public ActionResult GetProduct()
    {
        logger.LogInformation("Getting all products...");

        List<Product> products = productService.GetAllProducts();

        return Ok(products);
    }
}
