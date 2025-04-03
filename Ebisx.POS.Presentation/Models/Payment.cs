
namespace Ebisx.POS.Presentation.Models;

public class Payment
{
    public int Id { get; set; }

    public decimal AmountPaid { get; set; }
    public int PaymentTypeId { get; set; }
    public int OrderId { get; set; }
    public int? NonCashPaymentMethodID { get; set; }
    public NonCashPaymentMethod? NonCashPaymentMethod { get; set; }
}

