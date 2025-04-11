namespace Ebisx.POS.Presentation.DTOs.Customer;

// Customer DTOs
public class CustomerRequestDto
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? TinNumber { get; set; }
    public string? IdNumber { get; set; }
}
