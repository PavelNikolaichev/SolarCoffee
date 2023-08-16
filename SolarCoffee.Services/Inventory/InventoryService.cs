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

    public void CreateSnapshot()
    {
        throw new NotImplementedException();
    }

    public ServiceResponse<bool> DeleteCustomer(int id)
    {
        throw new NotImplementedException();
    }

    public List<ProductInventory> GetCurrentInventory()
    {
        return db.ProductInventories
            .Include(pi => pi.Product)
            .Where(pi => !pi.Product.IsArchieved)
            .ToList();
    }

    public ProductInventory? GetInventory(int productId)
    {
        throw new NotImplementedException();
    }

    public List<ProductInventorySnapshot> GetSnapshotHistory()
    {
        throw new NotImplementedException();
    }

    public ServiceResponse<ProductInventory?> UpdateUnitsAvailable(int id, int adjustment)
    {
        try
        {
            var inventory = db.ProductInventories
                .Include(pi => pi.Product)
                .First(pi => pi.Product.Id == id);

            inventory.QuantityOnHand += adjustment;

            try
            {
                CreateSnapshot();
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
                Message = $"Product {id} inventory adjusted!"
            };
        }
        catch (Exception ex)
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
