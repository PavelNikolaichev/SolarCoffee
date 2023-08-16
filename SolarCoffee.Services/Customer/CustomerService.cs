using Microsoft.EntityFrameworkCore;
using SolarCoffee.Data;
using SolarCoffee.Data.Models;

namespace SolarCoffee.Services.Customer;

public class CustomerService : ICustomerService
{
    private readonly SolarDbContext db;

    public CustomerService(SolarDbContext db)
    {
        this.db = db;
    }

    /// <summary>
    /// Adds a new customer record
    /// </summary>
    /// <param name="customer"></param>
    /// <returns></returns>
    public ServiceResponse<Data.Models.Customer?> CreateCustomer(Data.Models.Customer customer)
    {
        try
        {
            db.Customers.Add(customer);
            db.SaveChanges();

            return new ServiceResponse<Data.Models.Customer?>
            {
                Data = customer,
                IsSuccess = true,
                Message = "New customer added"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<Data.Models.Customer?>
            {
                Data = null,
                IsSuccess = false,
                Message = ex.StackTrace,
            };
        }
    }

    /// <summary>
    /// Deletes a customer record
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public ServiceResponse<bool> DeleteCustomer(int id)
    {
        var customer = this.GetCustomer(id);

        if (customer == null)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                IsSuccess = true,
                Message = "Customer not found",
            };
        }

        try
        {
            db.Customers.Remove(customer);
            db.SaveChanges();

            return new ServiceResponse<bool>
            {
                Data = true,
                IsSuccess = true,
                Message = "Customer deleted"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                IsSuccess = false,
                Message = ex.StackTrace
            };
        }
    }

    /// <summary>
    /// Returns a list of Customers from the database
    /// </summary>
    /// <returns></returns>
    public List<Data.Models.Customer> GetAllCustomers()
    {
        return db.Customers
            .Include(customer => customer.PrimaryAddress)
            .OrderBy(customer => customer.LastName)
            .ToList();
    }
    

    /// <summary>
    /// Gets a customer record by primary key
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Data.Models.Customer? GetCustomer(int id)
    {
        return db.Customers.FirstOrDefault(customer => customer.Id == id);
    }
}
