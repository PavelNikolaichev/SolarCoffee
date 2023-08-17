using System.ComponentModel.DataAnnotations;

namespace SolarCoffee.Web.ViewModels;


/// <summary>
/// View model for Orders
/// </summary>
public class CustomerModel
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime UpdatedOn { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public CustomerAddressModel PrimaryAddress { get; set; }
}

public class CustomerAddressModel
{
    public int Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string City { get; set; }

    public string Region { get; set; }
    
    public string PostalCode { get; set; }

    public string Country { get; set; }
}