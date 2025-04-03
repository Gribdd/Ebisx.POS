
namespace Ebisx.POS.Presentation.Models;

public class PaymentType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class Payment
{
    public int Id { get; set; }

    public decimal AmountPaid { get; set; }
    public int PaymentTypeId { get; set; }
    public Guid OrderId { get; set; }
    public int? NonCashPaymentMethodID { get; set; }
    public NonCashPaymentMethod? NonCashPaymentMethod { get; set; }
}

public class NonCashPaymentMethod
{
    public int Id { get; set; }
    public string Provider { get; set; } = string.Empty;
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? TinNumber { get; set; }
    public string? IdNumber { get; set; }
}

public class Discount
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public bool IsPercentage { get; set; }

    public int DiscountTypeId { get; set; }
    public DiscountType? DiscountType { get; set; }
}

public class DiscountType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

