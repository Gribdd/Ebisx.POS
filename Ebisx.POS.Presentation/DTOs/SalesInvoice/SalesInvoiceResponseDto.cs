namespace Ebisx.POS.Presentation.DTOs.SalesInvoice;

public class SalesInvoiceResponseDto
{
    public int PrivateId { get; set; }
    public string PublicId { get; set; } = string.Empty;
    public ICollection<PaymentResponseDto>? Payments { get; set; }
    public OrderResponseDto? Order { get; set; }
    public MachineInfoResponseDto? MachineInfo { get; set; }
    public BusinessInfoResponseDto? BusinessInfo { get; set; }
    public UserResponseDto? User { get; set; }
}
