﻿namespace Ebisx.POS.Presentation.Services.Interface;

public interface IPdfGeneratorService
{
    void GeneratePdf(WebView pdfview, BusinessInfo businessInfo, MachineInfo machineInfo, User user);
}
