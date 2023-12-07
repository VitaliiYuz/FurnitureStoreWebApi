using FurnitureStoreWebApi.Models;
using System.Collections.ObjectModel;

namespace FurnitureStoreWebApi.Interfaces
{
    public interface IOrderRepository
    {
        ICollection<Order> GetAllOrders();
        Order GetOrderById(int id);
        bool OrderExists(int id);
        bool CreateOrder(Order order);
        bool UpdateOrder(Order order);
        bool DeleteOrder(Order order);
        bool Save();
    }
}
