using Microsoft.AspNetCore.Mvc;
using SolarCoffee.Services.Customer;
using SolarCoffee.Services.Order;
using SolarCoffee.Web.Serializers;
using SolarCoffee.Web.ViewModels;
using System.Security.Cryptography.X509Certificates;
using SolarCoffee.Services.Inventory;

namespace SolarCoffee.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    private readonly ILogger<InventoryController> logger;
    private readonly IInventoryService inventoryService;

    public InventoryController(
        ILogger<InventoryController> logger,
        IInventoryService inventoryService
        ) {
        this.logger = logger;
        this.inventoryService = inventoryService;
    }

    [HttpGet("/api/inventory")]
    public ActionResult GetCurrentInventory()
    {
        logger.LogInformation("Getting inventory...");

        var inventory = inventoryService.GetCurrentInventory()
            .Select(product => new ProductInventoryModel
            {
                Id = product.Id,
                QuantityOnHand = product.QuantityOnHand,
                IdealQuantity = product.IdealQuantity,
                Product = ProductMapper.SerializeProductModel(product.Product)
            })
            .OrderBy(inv => inv.Product.Name)
            .ToList();

        return Ok(inventory);
    }

    [HttpPatch("/api/inventory")]
    public ActionResult UpdateInventory([FromBody] ShipmentModel shipment)
    {
        logger.LogInformation($"Updating inventory for {shipment.ProductId} -" +
                                $"Adjustment: {shipment.Adjustment}...");

        var inventory = inventoryService.UpdateUnitsAvailable(shipment.ProductId, shipment.Adjustment);

        return Ok(inventory);
    }
}
