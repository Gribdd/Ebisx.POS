namespace Ebisx.POS.Presentation.DTOs.MachineInfo;

// MachineInfo DTOs
public class MachineInfoRequestDto
{
    public string PosSerialNumber { get; set; } = string.Empty;
    public string MinNumber { get; set; } = string.Empty;
    public string AccreditationNumber { get; set; } = string.Empty;
    public string PtuNumber { get; set; } = string.Empty;
    public DateTime DateIssued { get; set; }
    public DateTime ValidUntil { get; set; }
}
