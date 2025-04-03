namespace Ebisx.POS.Presentation.Services.Interface;

public interface IBusinessInfoService
{
    Task<List<BusinessInfo>> GetBusinessInfosAsync();
    Task<BusinessInfo> GetBusinessInfoByIdAsync(int id);
    Task<bool> CreateBusinessInfoAsync(BusinessInfo businessInfo);
    Task<bool> DeleteBusinessInfoAsync(int id);
}
