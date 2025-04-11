namespace Ebisx.POS.Presentation.Models;

public partial class Order : ObservableObject
{
    public int Id { get; set; }

    public ObservableCollection<OrderItem> OrderItems { get; set; } = new ObservableCollection<OrderItem>();

    [ObservableProperty]
    public partial string Status { get; set; } = string.Empty;
}
