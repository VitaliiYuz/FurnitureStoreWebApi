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
            return _dataContext.Customer.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _dataContext.Customer.FirstOrDefault(p => p.Id == id);
        }

        public bool CustomerExists(int id)
        {
            return _dataContext.Customer.Any(p => p.Id == id);
        }

        public bool CreateCustomer(Customer customer)
        {
            if (!_dataContext.Any(t => t.Id = customer.CustomerId))
            {
                // Team with the provided TeamId does not exist, return an error
                return false;
            }
            _dataContext.Add(customer);
            return Save();
        }

        public bool UpdateCustomer(Customer customer)
        {
            if (!_dataContext.Any(t => t.Id == customer.CustomerId))
            {
                // Team with the provided TeamId does not exist, return an error
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
