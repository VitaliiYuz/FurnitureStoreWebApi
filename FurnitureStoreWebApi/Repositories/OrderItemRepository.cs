using FurnitureStoreWebApi.Interfaces;
using FurnitureStoreWebApi.Models;
namespace FurnitureStoreWebApi.Repositories
{
    public class OrderItemRepository:IOrderItemRepository
    {
        private readonly FurnitureStoreContext _dataContext;

        public OrderItemRepository(FurnitureStoreContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ICollection<OrderItem> GetAllOrderItems() 
        {
            return _dataContext.OrderItems.ToList();
        }
        public OrderItem GetOrderItemById(int id)
        {
            return _dataContext.OrderItems.FirstOrDefault(p => p.OrderItemId == id);
        }

        public bool OrderItemExists(int id)
        {
            return _dataContext.OrderItems.Any(p => p.OrderItemId == id);
        }

        public bool CreateOrderItem(OrderItem orderitem)
        {
            _dataContext.Add(orderitem);
            return Save();
        }

        public bool UpdateOrderItem(OrderItem orderitem)
        {
            if (!_dataContext.OrderItems.Any(t => t.OrderItemId == orderitem.OrderItemId))
            {
                return false;
            }
            _dataContext.Update(orderitem);
            return Save();
        }
        public bool DeleteOrderItem(OrderItem orderitem)
        {
            _dataContext.Remove(orderitem);
            return Save();
        }
        public bool Save()
        {
            return _dataContext.SaveChanges() > 0;
        }
    }
}
