
namespace Ebisx.POS.Presentation.Services.Interface;

public interface IPaymentService
{
    Task<List<Payment>> GetPaymentsAsync();
    Task<Payment> GetPaymentByIdAsync(int id);
    Task<Payment> CreatePaymentAsync(Payment payment);
    Task<bool> DeletePaymentAsync(int id);
}
