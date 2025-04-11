namespace Ebisx.POS.Presentation.Services.Interface;

public interface IOrderService
{
    Task<List<Order>> GetOrdersAsync();
    Task<Order> GetOrderByIdAsync(int id);
    Task<Order> CreateOrderAsync(Order order);
    Task<Order> AddItemsToOrder(int orderId, List<OrderItem> newItems);
    Task<bool> DeleteOrderAsync(int id);
    Task<bool> UpdateOrderAsync(Order order);
}
