using SolarCoffee.Data.Models;
using SolarCoffee.Web.ViewModels;

namespace SolarCoffee.Web.Serializers;

public class CustomerMapper
{
    /// <summary>
    /// Maps an InvoiceModel view model to a SalesOrder data model
    /// </summary>
    /// <param name="invoice"></param>
    /// <returns></returns>
    public static CustomerModel SerializeCustomer(Customer customer)
    {
        var address = new CustomerAddressModel
        {
            Id = customer.Id,
            AddressLine1 = customer.PrimaryAddress.AddressLine1,
            AddressLine2 = customer.PrimaryAddress.AddressLine2,
            City = customer.PrimaryAddress.City,
            Region = customer.PrimaryAddress.Region,
            Country = customer.PrimaryAddress.Country,
            PostalCode = customer.PrimaryAddress.PostalCode,
            CreatedOn = customer.PrimaryAddress.CreatedOn,
            UpdatedOn = customer.PrimaryAddress.UpdatedOn,
        };

        return new CustomerModel
        {
            Id = customer.Id,
            CreatedOn = customer.CreatedOn,
            UpdatedOn = customer.UpdatedOn,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PrimaryAddress = address,
        };
    }

    public static Customer SerializeCustomer(CustomerModel customer)
    {
        var address = new CustomerAddress
        {
            Id = customer.Id,
            AddressLine1 = customer.PrimaryAddress.AddressLine1,
            AddressLine2 = customer.PrimaryAddress.AddressLine2,
            City = customer.PrimaryAddress.City,
            Region = customer.PrimaryAddress.Region,
            Country = customer.PrimaryAddress.Country,
            PostalCode = customer.PrimaryAddress.PostalCode,
            CreatedOn = customer.PrimaryAddress.CreatedOn,
            UpdatedOn = customer.PrimaryAddress.UpdatedOn,
        };

        return new Customer
        {
            Id = customer.Id,
            CreatedOn = customer.CreatedOn,
            UpdatedOn = customer.UpdatedOn,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            PrimaryAddress = address,
        };
    }
}