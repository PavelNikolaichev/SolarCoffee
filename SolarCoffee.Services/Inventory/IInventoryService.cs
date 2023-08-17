using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Inventory;

public interface IInventoryService
{
    List<Data.Models.ProductInventory> GetCurrentInventory();

    ServiceResponse<Data.Models.ProductInventory?> UpdateUnitsAvailable(int id, int adjustment);

    Data.Models.ProductInventory? GetInventory(int productId);

    public List<ProductInventorySnapshot> GetSnapshotHistory();
}
