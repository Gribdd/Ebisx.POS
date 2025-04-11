namespace Ebisx.POS.Presentation.DTOs.Payment;

public class PaymentRequestDto
{
    public decimal AmountPaid { get; set; }
    public int PaymentTypeId { get; set; }
    public int OrderId { get; set; }
    public int? NonCashPaymentMethodID { get; set; }
    public int? CustomerId { get; set; }
}
