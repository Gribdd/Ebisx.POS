
namespace Ebisx.POS.Presentation.Models;

public partial class PaymentMethod : BaseModel
{
    public string? Name { get; set; }
    public decimal Amount { get; set; }
}
