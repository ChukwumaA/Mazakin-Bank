using MazikeenDbLayer.Model;

namespace MazikeenDbLayer.Interfaces;

public interface ICustomerAtmService : IDisposable
{
    Task<int> AddCustomer(CustomerModel customer);
    Task<bool> UpdateCustomer(int id, CustomerModel customerAtm);
    Task<bool> DeleteCustomer(int id);
    Task<CustomerModel> GetCustomer(int id);
    Task<IEnumerable<CustomerModel>> GetAllCustomers();
}