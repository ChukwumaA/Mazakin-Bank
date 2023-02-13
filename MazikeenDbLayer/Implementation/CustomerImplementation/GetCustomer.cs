using MazikeenDbLayer.Interfaces;
using MazikeenDbLayer.Services;

namespace MazikeenDbLayer.Implementation.CustomerImplementation;

public class GetCustomer
{
    public static async Task CustomerRetrieved()
    {
        using ICustomerAtmService customerAtmService = new CustomerAtmService(new AtmDbContext());

        var customer = await customerAtmService.GetCustomer(4);
        Console.WriteLine($"Customer {customer.customer_id} retrieved successfully" +
                          $" WITH FIRST NAME: {customer.firstName} and LAST NAME: {customer.lastName}" +
                          $" is {customer.gender}, {customer.age} years old, and lives at {customer.address}");
    }
}