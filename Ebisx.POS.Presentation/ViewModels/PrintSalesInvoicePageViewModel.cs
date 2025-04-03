namespace Ebisx.POS.Presentation.ViewModels;

public partial class PrintSalesInvoicePageViewModel : BaseViewModel
{
    private readonly IPdfGeneratorService _pdfGeneratorService;
    private readonly IMachineInfoService _machineInfoService;
    private readonly IBusinessInfoService _businessInfoService;
    private readonly IUserService _userService;
    private readonly ISettingsService _settingsService;

    [ObservableProperty]
    public partial BusinessInfo BusinessInfo { get; set; } = new();
    [ObservableProperty]
    public partial MachineInfo MachineInfo { get; set; } = new();
    [ObservableProperty]
    public partial User User { get; set; } = new();

    public PrintSalesInvoicePageViewModel(
        IPdfGeneratorService pdfGeneratorService,
        IMachineInfoService machineInfoService,
        IBusinessInfoService businessInfoService,
        IUserService userService,
        ISettingsService settingsService
        )
    {
        _pdfGeneratorService = pdfGeneratorService;
        _machineInfoService = machineInfoService;
        _businessInfoService = businessInfoService;
        _userService = userService;
        _settingsService = settingsService;
    }


    [RelayCommand]
    private void GeneratePdf(WebView pdfview)
    {
        _pdfGeneratorService.GeneratePdf(
            pdfview,
            BusinessInfo,
            MachineInfo,
            User);
    }

    public async Task InitializeValues()
    {
        BusinessInfo = await _businessInfoService.GetBusinessInfoByIdAsync(1);
        MachineInfo = await _machineInfoService.GetMachineInfoByIdAsync(1);
        User = await _userService.GetUserByIdAsync(_settingsService.AuthUserId);
    }

}
