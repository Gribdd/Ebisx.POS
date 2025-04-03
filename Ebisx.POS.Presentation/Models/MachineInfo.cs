
namespace Ebisx.POS.Presentation.Models;

public class MachineInfo
{
    public int Id { get; set; }
    public string PosSerialNumber { get; set; } = string.Empty;
    public string MinNumber { get; set; } = string.Empty;
    public string AccreditationNumber { get; set; } = string.Empty;
    public string PtuNumber { get; set; } = string.Empty;
    public DateTime DateIssued { get; set; }
    public DateTime ValidUntil { get; set; }
}

