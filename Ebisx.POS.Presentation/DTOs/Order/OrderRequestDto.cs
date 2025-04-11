using Ebisx.POS.Presentation.DTOs.OrderItem;

namespace Ebisx.POS.Presentation.DTOs.Order;

// Order DTOs
public class OrderRequestDto
{
    public ICollection<OrderItemRequestDto> OrderItems { get; set; } = new List<OrderItemRequestDto>();
    public string Status { get; set; } = string.Empty;
}
