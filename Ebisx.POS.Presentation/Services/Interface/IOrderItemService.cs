namespace Ebisx.POS.Presentation.Services.Interface;

public interface IOrderItemService
{
    Task<List<OrderItem>> GetOrderItemsAsync();
    Task<OrderItem?> GetOrderItemByIdAsync(int id);
    Task<OrderItem?> CreateOrderItemAsync(OrderItem orderItem);
    Task<bool> UpdateOrderItemAsync(int id, OrderItem orderItem);
    Task<bool> DeleteOrderItemAsync(int id);
}
