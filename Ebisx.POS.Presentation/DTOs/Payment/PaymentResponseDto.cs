

namespace Ebisx.POS.Presentation.DTOs.Payment;

public class PaymentResponseDto
{
    public int Id { get; set; }
    public decimal AmountPaid { get; set; }
    public PaymentTypeResponseDto PaymentType { get; set; } = null!;
    public int OrderId { get; set; }
    public NonCashPaymentMethodResponseDto? NonCashPaymentMethod { get; set; }
    public CustomerResponseDto? Customer { get; set; }
}
