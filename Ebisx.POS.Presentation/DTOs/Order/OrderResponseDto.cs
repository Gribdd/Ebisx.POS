using Ebisx.POS.Presentation.DTOs.OrderItem;

namespace Ebisx.POS.Presentation.DTOs.Order;

public class OrderResponseDto
{
    public int Id { get; set; }
    public ICollection<OrderItemResponseDto> OrderItems { get; set; } = new List<OrderItemResponseDto>();
    public string Status { get; set; } = string.Empty;
}
