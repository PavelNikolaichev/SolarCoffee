using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Product;

public class ProductService : IProductService
{
    private readonly SolarDbContext db;

    public ProductService(SolarDbContext db)
    {
        this.db = db;
    }

    /// <summary>
    /// Retrieves all products from the db
    /// </summary>
    /// <returns>List of Product instances</returns>
    public List<Data.Models.Product> GetAllProducts()
    {
        return db.Products.ToList();
    }

    /// <summary>
    /// Retrieves product by its id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Product instance or null in case nothing was found</returns>
    public Data.Models.Product? GetProduct(int id)
    {
        return db.Products.Find(id);
    }

    /// <summary>
    /// Add a new product to the database
    /// </summary>
    /// <param name="product"></param>
    /// <returns>Product instance or null in case of error</returns>
    public ServiceResponse<Data.Models.Product?> CreateProduct(Data.Models.Product product)
    {
        try {
            db.Products.Add(product);

            ProductInventory newInventory = new ProductInventory
            {
                Product = product,
                QuantityOnHand = 0,
                IdealQuantity = 10,
            };
            db.ProductInventories.Add(newInventory);

            db.SaveChanges();

            return new ServiceResponse<Data.Models.Product?>
            {
                Data = product,
                Message = "Product saved.",
                IsSuccess = true
            };
        }
        catch (Exception ex) {
            return new ServiceResponse<Data.Models.Product?>
            {
                Data = null,
                Message = ex.StackTrace ?? "",
                IsSuccess = false
            };
        }
    }

    /// <summary>
    /// Archieve product.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Product instance or null in case of error</returns>
    public ServiceResponse<Data.Models.Product?> ArchiveProduct(int id)
    {
        Data.Models.Product? product = this.GetProduct(id);

        if (product == null)
        {
            return new ServiceResponse<Data.Models.Product?>
            {
                Data = null,
                Message = "Product with this id was not found.",
                IsSuccess = false
            };
        }
        
        product.IsArchieved = true;
        db.SaveChanges();

        return new ServiceResponse<Data.Models.Product?>
        {
            Data = product,
            Message = "Product archieved",
            IsSuccess = true
        };
    }

}
