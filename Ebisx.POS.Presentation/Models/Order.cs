namespace Ebisx.POS.Presentation.Models;

public partial class Order : ObservableObject
{
    public int Id { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ObservableProperty]
    public partial string Status { get; set; } = string.Empty;
}
