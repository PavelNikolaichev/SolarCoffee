using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Inventory;

public class InventoryService : IInventoryService
{
    private readonly SolarDbContext db;
    private readonly ILogger<InventoryService> logger;

    public InventoryService(SolarDbContext db, ILogger<InventoryService> logger)
    {
        this.db = db;
        this.logger = logger;
    }

    private void CreateSnapshot(ProductInventory inventory)
    {
        var snapshot = new ProductInventorySnapshot
        {
            SnapshotTime = DateTime.UtcNow,
            Product = inventory.Product,
            QuantityOnHand = inventory.QuantityOnHand,
        };

        db.Add(snapshot);
    }

    /// <summary>
    /// Returns list of ProductInventory models
    /// </summary>
    /// <returns></returns>
    public List<ProductInventory> GetCurrentInventory()
    {
        return db.ProductInventories
            .Include(pi => pi.Product)
            .Where(pi => !pi.Product.IsArchieved)
            .ToList();
    }

    /// <summary>
    /// Returns inventory by product id
    /// </summary>
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public ProductInventory? GetInventory(int productId)
    {
        return db.ProductInventories
                .Include(pi => pi.Product)
                .FirstOrDefault(pi => pi.Product.Id == productId);
    }

    /// <summary>
    /// Returns list of ProductInventorySnapshot models
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public List<ProductInventorySnapshot> GetSnapshotHistory()
    {
        // E.g. We filterl snapshots by last x hours
        var from = DateTime.UtcNow - TimeSpan.FromHours(4);

        return db.ProductInventorySnapshots
            .Include(snapshot => snapshot.Product)
            .Where(snapshot => snapshot.SnapshotTime > from && !snapshot.Product.IsArchieved)
            .ToList();
    }

    /// <summary>
    /// Update available units for record
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="adjustment"></param>
    /// <returns></returns>
    public ServiceResponse<ProductInventory?> UpdateUnitsAvailable(int productId, int adjustment)
    {
        try
        {
            var inventory = this.GetInventory(productId);

            inventory.QuantityOnHand += adjustment;

            try
            {
                CreateSnapshot(inventory);
            }
            catch (Exception ex)
            {
                logger.LogError("Error creating snapshot");
                logger.LogError(ex.StackTrace);
            }

            db.SaveChanges();

            return new ServiceResponse<ProductInventory?>
            {
                IsSuccess = true,
                Data = inventory,
                Message = $"Product {productId} inventory adjusted!"
            };
        }
        catch (Exception)
        {
            return new ServiceResponse<ProductInventory?>
            {
                IsSuccess = false,
                Data = null,
                Message = $"Error updating ProductInventory QuantityOnHand"
            };
        }
    }
}
