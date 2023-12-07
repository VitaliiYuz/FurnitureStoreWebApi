using FurnitureStoreWebApi.Models;

namespace FurnitureStoreWebApi.Interfaces
{
    public interface IOrderItemRepository
    {
        ICollection<OrderItem> GetAllOrderItems();
        OrderItem GetOrderItemById(int id);
        bool OrderItemExists(int id);
        bool CreateOrderItem(OrderItem orderItem);
        bool UpdateOrderItem(OrderItem orderItem);
        bool DeleteOrderItem(OrderItem orderItem);
        bool Save();
    }
}
