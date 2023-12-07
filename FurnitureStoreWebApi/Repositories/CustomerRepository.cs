using FurnitureStoreWebApi.Models;
using FurnitureStoreWebApi.Interfaces;
using System.Numerics;

namespace FurnitureStoreWebApi.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FurnitureStoreContext _dataContext;

        public CustomerRepository(FurnitureStoreContext dataContext)
        {
            _dataContext = dataContext;
        }
        public ICollection<Customer> GetAllCustomers()
        {
            return _dataContext.Customers.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _dataContext.Customers.FirstOrDefault(p => p.CustomerId == id);
        }

        public bool CustomerExists(int id)
        {
            return _dataContext.Customers.Any(p => p.CustomerId == id);
        }

        public bool CreateCustomer(Customer customer)
        {
            _dataContext.Add(customer);
            return Save();
        }

        public bool UpdateCustomer(Customer customer)
        {
            if (!_dataContext.Customers.Any(t => t.CustomerId == customer.CustomerId))
            {
                return false;
            }
            _dataContext.Update(customer);
            return Save();
        }
        public bool DeleteCustomer(Customer customer)
        {
            _dataContext.Remove(customer);
            return Save();
        }
        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }

    }
}
