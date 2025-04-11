namespace Ebisx.POS.Presentation.DTOs.Customer;

public class CustomerResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string? TinNumber { get; set; }
    public string? IdNumber { get; set; }
}
