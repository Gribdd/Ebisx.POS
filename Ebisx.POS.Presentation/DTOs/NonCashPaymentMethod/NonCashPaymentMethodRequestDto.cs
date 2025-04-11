namespace Ebisx.POS.Presentation.DTOs.NonCashPaymentMethod;

// NonCashPaymentMethod DTOs
public class NonCashPaymentMethodRequestDto
{
    public string Provider { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
}
