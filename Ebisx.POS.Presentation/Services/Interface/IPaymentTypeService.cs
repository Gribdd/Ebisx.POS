
namespace Ebisx.POS.Presentation.Services.Interface;

public interface IPaymentTypeService
{
    Task<List<PaymentType>> GetPaymentTypesAsync();
    Task<PaymentType> GetPaymentTypeByIdAsync(int id);
    Task<bool> CreatePaymentTypeAsync(PaymentType paymentType);
    Task<bool> DeletePaymentTypeAsync(int id);
}
