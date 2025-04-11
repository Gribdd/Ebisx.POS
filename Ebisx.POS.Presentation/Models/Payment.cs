namespace Ebisx.POS.Presentation.Models;

public class Payment
{
    public int Id { get; set; }

    public decimal AmountPaid { get; set; }
    public int PaymentTypeId { get; set; }
    public PaymentType? PaymentType { get; set; }   
    public int OrderId { get; set; }
    public int? NonCashPaymentMethodID { get; set; }
    public NonCashPaymentMethod? NonCashPaymentMethod { get; set; }

    public Customer? Customer { get; set; }
    public int? CustomerId { get; set; }
}

