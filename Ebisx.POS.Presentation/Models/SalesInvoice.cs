using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ebisx.POS.Presentation.Models;
public class SalesInvoice
{
    public int PrivateId { get; set; }

    public string PublicId { get; set; } = string.Empty;
    public Order? Order { get; set; }

    public ICollection<Payment>? Payments { get; set; }

    public MachineInfo? MachineInfo { get; set; }

    public BusinessInfo? BusinessInfo { get; set; }
    public User? User { get; set; }

}

