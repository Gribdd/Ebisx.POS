namespace Ebisx.POS.Presentation.Services.Interface;

public interface IMachineInfoService
{
    Task<List<MachineInfo>> GetMachineInfosAsync();
    Task<MachineInfo> GetMachineInfoByIdAsync(int id);
    Task<bool> CreateMachineInfoAsync(MachineInfo machineInfo);
    Task<bool> DeleteMachineInfoAsync(int id);
}
