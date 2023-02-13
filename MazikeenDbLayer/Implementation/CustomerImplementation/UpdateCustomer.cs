using MazikeenDbLayer.Interfaces;
using MazikeenDbLayer.Model;
using MazikeenDbLayer.Services;

namespace MazikeenDbLayer.Implementation.CustomerImplementation;

public static class UpdateCustomer
{
    public static async Task CustomerUpdated()
    {
        using ICustomerAtmService customerAtmService = new CustomerAtmService(new AtmDbContext());

        var updateCustomer = await customerAtmService.UpdateCustomer(1, new CustomerModel()
        {
            customer_id = 1,
            firstName = "Edgar",
            lastName = "Stark",
            address = "No 1 Winterfell, North, Westeros",
            gender = "Female",
            age = 31
        });
        var customerUpdatedSuccessfully = (updateCustomer ? "Customer updated successfully" : "Customer update failed");
        Console.WriteLine(customerUpdatedSuccessfully);
    }
}