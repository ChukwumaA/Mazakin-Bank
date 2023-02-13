using MazikeenDbLayer.Interfaces;
using MazikeenDbLayer.Services;

namespace MazikeenDbLayer.Implementation.CustomerImplementation;

public static class GetAllCustomers
{
    public static async Task AllCustomersRetrieved()
    {
        using ICustomerAtmService customerAtmService = new CustomerAtmService(new AtmDbContext());
        var allCustomers = await customerAtmService.GetAllCustomers();

        foreach (var customer in allCustomers)
        {
            Console.WriteLine($"Customer {customer.customer_id} retrieved successfully" +
                              $" WITH FIRST NAME: {customer.firstName} and LAST NAME: {customer.lastName}" +
                              $" is {customer.gender}, {customer.age} years old, and lives at {customer.address}");

        }
    }
}