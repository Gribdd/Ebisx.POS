namespace Ebisx.POS.Presentation.DTOs.NonCashPaymentMethod;

public class NonCashPaymentMethodResponseDto
{
    public int Id { get; set; }
    public string Provider { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
}
