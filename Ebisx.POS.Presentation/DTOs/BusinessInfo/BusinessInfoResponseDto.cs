namespace Ebisx.POS.Presentation.DTOs.BusinessInfo;

public class BusinessInfoResponseDto
{
    public int Id { get; set; }
    public string RegistedName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string VatTinNumber { get; set; } = string.Empty;
}
