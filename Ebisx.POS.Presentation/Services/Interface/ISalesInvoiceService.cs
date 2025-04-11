namespace Ebisx.POS.Presentation.Services.Interface;

public interface ISalesInvoiceService
{
    Task<List<SalesInvoice>> GetSalesInvoicesAsync();
    Task<SalesInvoice> GetSalesInvoiceByIdAsync(int id);
    Task<bool> CreateSalesInvoiceAsync(SalesInvoice salesInvoice);
    Task<bool> DeleteSalesInvoiceAsync(int id);
}
