using MazikeenDbLayer.Interfaces;
using MazikeenDbLayer.Services;

namespace MazikeenDbLayer.Implementation.CustomerImplementation;

public static class DeleteCustomer
{
    public static async Task CustomerDeleted()
    {
        using ICustomerAtmService customerAtmService = new CustomerAtmService(new AtmDbContext());

        var deleteCustomer = await customerAtmService.DeleteCustomer(4);
        var customerDeletedSuccessfully = (deleteCustomer ? "Customer deleted successfully" : "Customer delete failed");
        Console.WriteLine(customerDeletedSuccessfully);
    }
}