using MazikeenDbLayer.Interfaces;
using MazikeenDbLayer.Model;
using MazikeenDbLayer.Services;

namespace MazikeenDbLayer.Implementation.CustomerImplementation;

public static class CreateCustomer
{
    public static async Task CustomerCreated()
        {
            using ICustomerAtmService customerAtmService = new CustomerAtmService(new AtmDbContext());
            var customerData = new CustomerModel
            {
                customer_id = 4,
                firstName = "Khal",
                lastName = "Drogon",
                address = "No 2, Dothraki, Bravos",
                gender = "Male",
                age = 33
            };
            var createdCustomer = await customerAtmService.AddCustomer(customerData);
            Console.WriteLine($"Created customer with id: {createdCustomer}");
        }
}