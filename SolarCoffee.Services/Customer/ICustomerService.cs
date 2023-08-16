namespace SolarCoffee.Services.Customer;

public interface ICustomerService
{
    List<Data.Models.Customer> GetAllCustomers();

    ServiceResponse<Data.Models.Customer?> CreateCustomer(Data.Models.Customer customer);

    ServiceResponse<bool> DeleteCustomer(int id);

    Data.Models.Customer? GetCustomer(int id);
}
