using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Inventory;

public interface IInventoryService
{
    List<Data.Models.ProductInventory> GetCurrentInventory();

    ServiceResponse<Data.Models.ProductInventory?> UpdateUnitsAvailable(int id, int adjustment);

    ServiceResponse<bool> DeleteCustomer(int id);

    Data.Models.ProductInventory? GetInventory(int productId);

    public void CreateSnapshot();

    public List<ProductInventorySnapshot> GetSnapshotHistory();
}
