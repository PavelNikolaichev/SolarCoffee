using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serializers;

public class ProductMapper
{
    /// <summary>
    /// Maps a Product data model to a ProductModel view model
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public static ProductModel SerializeProductModel(Data.Models.Product product)
    {
        return new ProductModel {
            Id = product.Id,
            Name = product.Name,
            CreatedOn = product.CreatedOn,
            UpdatedOn = product.UpdatedOn,
            Price = product.Price,
            Description = product.Description,
            IsTaxable = product.IsTaxable,
            IsArchieved = product.IsArchieved,
        };
    }


    /// <summary>
    /// Maps a ProductModel view model to a Product data model
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public static Data.Models.Product SerializeProductModel(ProductModel product)
    {
        return new Data.Models.Product
        {
            Id = product.Id,
            Name = product.Name,
            CreatedOn = product.CreatedOn,
            UpdatedOn = product.UpdatedOn,
            Price = product.Price,
            Description = product.Description,
            IsTaxable = product.IsTaxable,
            IsArchieved = product.IsArchieved,
        };
    }
}