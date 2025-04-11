namespace Ebisx.POS.Presentation.DTOs.Discount;

// Discount DTOs
public class DiscountRequestDto
{
    public string Value { get; set; } = string.Empty;
    public bool IsPercentage { get; set; }
    public int DiscountTypeId { get; set; }
}
