using FurnitureStoreWebApi.Models;
using System.Numerics;

namespace FurnitureStoreWebApi.Interfaces
{
    public interface ICustomerRepository
    {
        ICollection<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        bool CustomerExists(int id);
        bool CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(Customer customer);
        bool Save();

    }
}
