
namespace Ebisx.POS.Presentation.Models;

public class Discount
{
    public int Id { get; set; }
    public string Value { get; set; } = string.Empty;
    public bool IsPercentage { get; set; }

    public int DiscountTypeId { get; set; }
    public DiscountType? DiscountType { get; set; }
}

