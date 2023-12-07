using FurnitureStoreWebApi.Interfaces;
using FurnitureStoreWebApi.Models;

namespace FurnitureStoreWebApi.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly FurnitureStoreContext _dataContext;

        public OrderRepository(FurnitureStoreContext dataContext)
        {
            _dataContext = dataContext;
        }
        public ICollection<Order> GetAllOrders() 
        {
            return _dataContext.Orders.ToList();
        }
        public Order GetOrderById(int id)
        {
            return _dataContext.Orders.FirstOrDefault(p => p.OrderId == id);
        }

        public bool OrderExists(int id)
        {
            return _dataContext.Orders.Any(p => p.OrderId == id);
        }

        public bool CreateOrder(Order order)
        {
            _dataContext.Add(order);
            return Save();
        }

        public bool UpdateOrder(Order order)
        {
            if (!_dataContext.Orders.Any(t => t.OrderId == order.OrderId))
            {
                return false;
            }
            _dataContext.Update(order);
            return Save();
        }
        public bool DeleteOrder(Order order)
        {
            _dataContext.Remove(order);
            return Save();
        }
        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }
    }
}
