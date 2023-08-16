using SolarCoffee.Data.Models;

namespace SolarCoffee.Services;

public class ServiceResponse<T>
{
    public bool IsSuccess { get; set; } = false;

    public string? Message { get; set; } = string.Empty;

    public DateTime Time { get; set; } = DateTime.UtcNow;

    public T? Data { get; set; }
}
